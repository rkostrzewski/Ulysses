using System;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class BilateralFilterAlgorithm : IImageProcessingAlgorithm
    {
        private readonly ImageModel _imageModel;
        private readonly uint _downsampledHeight;
        private readonly uint _downsampledRange;
        private readonly uint _downsampledWidth;
        private readonly uint _height;
        private readonly uint _range;
        private readonly uint _rangeKernel;
        private readonly uint _spatialKernel;
        private readonly uint _width;
        private readonly uint[,,] _w;
        private readonly uint[,,] _wi;
        private readonly uint[,,] _wig;
        private readonly uint[,,] _wg;

        public BilateralFilterAlgorithm(BilateralFilterTemplate template)
        {
            _imageModel = template.ImageModel;
            _rangeKernel = template.RangeKernel;
            _spatialKernel = template.SpatialKernel;
            _height = template.ImageModel.Height;
            _width = template.ImageModel.Width;
            _range = 4096;

            _downsampledWidth = _width / _spatialKernel + 1;
            _downsampledHeight = _height / _spatialKernel + 1;
            _downsampledRange = _range / _rangeKernel + 1;

            _wi = new uint[_downsampledHeight, _downsampledWidth, _downsampledRange];
            _wig = new uint[_downsampledHeight, _downsampledWidth, _downsampledRange];
            _w = new uint[_downsampledHeight, _downsampledWidth, _downsampledRange];
            _wg = new uint[_downsampledHeight, _downsampledWidth, _downsampledRange];

        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            double[] weightedDifferenceOfFilter;
            return BilateralFiletring(inputImage.ImagePixels, out weightedDifferenceOfFilter);
        }

        public Image BilateralFiletring(Pixels inputImage, out double[] weightedDifferenceOfFilter)
        {
            for (int i = 0, l = 0; i < _height; i++)
            {
                for (ushort j = 0; j < _width; j++, l++)
                {
                    var x = Math.Min(i / _spatialKernel, _downsampledHeight - 1);
                    var y = Math.Min(j / _spatialKernel, _downsampledWidth - 1);
                    var z = Math.Min(inputImage[l] / _rangeKernel, _downsampledRange - 1);

                    _wi[x, y, z] += inputImage[l];
                    _w[x, y, z] += 1;
                }
            }


            for (ushort x = 0; x < _downsampledHeight; x++)
            {
                for (ushort y = 0; y < _downsampledWidth; y++)
                {
                    for (ushort z = 0; z < _downsampledRange; z++)
                    {
                        var zLeft2 = Math.Max(0, z - 2);
                        var zLeft1 = Math.Max(0, z - 1);
                        var zRight1 = Math.Min(_downsampledRange - 1, z + 1);
                        var zRight2 = Math.Min(_downsampledRange - 1, z + 2);

                        // Only range is taken into account. Drastically improves speed without significant quality loss (for noisy images).
                        _wig[x, y, z] = _wi[x, y, zLeft2] + 4 * _wi[x, y, zLeft1] + 6 * _wi[x, y, z] + 4 * _wi[x, y, zRight1] + _wi[x, y, zRight2];
                        _wg[x, y, z] = _w[x, y, zLeft2] + 4 * _w[x, y, zLeft1] + 6 * _w[x, y, z] + 4 * _w[x, y, zRight1] + _w[x, y, zRight2];
                    }
                }
            }


            weightedDifferenceOfFilter = new double[_width * _height];
            var k = 0;
            var bilateralFilteredImage = new Pixel[_width * _height];
            for (ushort i = 0; i < _height; i++)
            {
                for (ushort j = 0; j < _width; j++, k++)
                {
                    var z = (ushort)Math.Min(inputImage[k], _range);
                    var weight = TriLinearInterpolation(_wg, i, j, z);
                    weightedDifferenceOfFilter[k] = 1 / weight;
                    bilateralFilteredImage[k] = (Pixel)(TriLinearInterpolation(_wig, i, j, z) / weight);
                }
            }

            return new Image(bilateralFilteredImage, _imageModel);
        }

        private double TriLinearInterpolation(uint[,,] source, ushort x, ushort y, ushort z)
        {
            var x0 = x / _spatialKernel;
            var x1 = x0 + 1;
            var xd = (x / _spatialKernel - x0) / (x1 - x0);
            if (xd == 0)
            {
                x1 = x0;
            }

            var y0 = y / _spatialKernel;
            var y1 = y0 + 1;
            var yd = (y / _spatialKernel - y0) / (y1 - y0);
            if (yd == 0)
            {
                y1 = 0;
            }
            var z0 = z / _rangeKernel;
            var z1 = z0 + 1;
            var zd = (z / _rangeKernel - z0) / (z1 - z0);
            if (zd == 0)
            {
                z1 = z0;
            }

            var c00 = source[x0, y0, z0] * (1 - xd) + source[x1, y0, z0] * xd;
            var c10 = source[x0, y1, z0] * (1 - xd) + source[x1, y1, z0] * xd;
            var c01 = source[x0, y0, z1] * (1 - xd) + source[x1, y0, z1] * xd;
            var c11 = source[x0, y1, z1] * (1 - xd) + source[x1, y1, z1] * xd;

            var c0 = c00 * (1 - yd) + c10 * yd;
            var c1 = c01 * (1 - yd) + c11 * yd;

            return c0 * (1 - zd) + c1 * zd;
        }
    }
}