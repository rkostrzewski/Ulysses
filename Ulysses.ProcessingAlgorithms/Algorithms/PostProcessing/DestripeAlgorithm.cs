using System;
using System.Collections.Generic;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class DestripeAlgorithm : IImageProcessingAlgorithm
    {
        private readonly byte _byteShiftValue;
        private readonly ImageModel _imageModel;
        private readonly ushort _pixelMaxValue;
        private readonly ushort _pixelMinValue;
        private readonly ushort _radius;

        public DestripeAlgorithm(DestripeTemplate template)
        {
            _imageModel = template.ImageModel;
            _radius = template.Radius;
            _pixelMinValue = 0;

            switch (_imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    _byteShiftValue = 10;
                    _pixelMaxValue = 255;
                    break;
                case ImageBitDepth.Bpp12:
                    _byteShiftValue = 14;
                    _pixelMaxValue = 4095;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            var cumulativeHistogram = CumulativeHistogram(inputImage);
            var columnCorrectionFactors = ColumnCorrectionFactor(cumulativeHistogram);

            return CorrectImage(inputImage, columnCorrectionFactors);
        }

        private Image CorrectImage(Image inputImage, IReadOnlyList<long> columnCorrectionFactors)
        {
            var width = inputImage.ImageModel.Width;
            var height = inputImage.ImageModel.Height;

            var destripedPixels = new Pixel[height * width];

            for (int row = 0, iterator = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++, iterator++)
                {
                    var inputPixel = inputImage.ImagePixels[iterator];
                    var correctionValue = (inputPixel * columnCorrectionFactors[column]) >> _byteShiftValue;
                    var pixelValue = (Pixel)(inputPixel + correctionValue);

                    destripedPixels[iterator] = Math.Min(_pixelMaxValue, Math.Max(_pixelMinValue, pixelValue));
                }
            }

            return new Image(destripedPixels, inputImage.ImageModel);
        }

        private long[] ColumnCorrectionFactor(IReadOnlyList<long> cumulativeHistogram)
        {
            var width = cumulativeHistogram.Count;
            var columnCorrectionFactors = new long[width];
            var extend = _radius / 2;
            long sum = 0;
            ushort count = 0;

            for (var arrayIndex = -extend; arrayIndex < width; arrayIndex++)
            {
                if (arrayIndex + extend < width)
                {
                    sum += cumulativeHistogram[arrayIndex + extend];
                    count++;
                }

                if (arrayIndex - extend >= 0)
                {
                    sum -= cumulativeHistogram[arrayIndex - extend];
                    count--;
                }

                if (arrayIndex < 0)
                {
                    continue;
                }

                if (cumulativeHistogram[arrayIndex] != 0)
                {
                    columnCorrectionFactors[arrayIndex] = ((sum / count - cumulativeHistogram[arrayIndex]) << _byteShiftValue) / cumulativeHistogram[arrayIndex];
                }
                else
                {
                    columnCorrectionFactors[arrayIndex] = long.MaxValue;
                }
            }

            return columnCorrectionFactors;
        }

        private static long[] CumulativeHistogram(Image inputImage)
        {
            var width = inputImage.ImageModel.Width;
            var height = inputImage.ImageModel.Height;

            var histogram = new long[width];
            for (uint i = 0; i < width; i++)
            {
                histogram[i] = 0;
            }

            for (int row = 0, iterator = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++, iterator++)
                {
                    histogram[column] += inputImage.ImagePixels[iterator];
                }
            }

            return histogram;
        }
    }
}