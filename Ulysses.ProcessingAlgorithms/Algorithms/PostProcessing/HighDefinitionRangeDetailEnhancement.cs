using System;
using System.Collections.Generic;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class HighDefinitionRangeDetailEnhancement : IImageProcessingAlgorithm, ITransformsImageModel
    {
        private readonly BilateralFilterAlgorithm _bilateralFilter;
        private readonly uint _binaryHistogramThreshold;
        private readonly double _detailsMaxGain;
        private readonly double _detailsMinGain;
        private readonly ushort _height;
        private readonly ImageModel _imageModel;
        private readonly ushort _range;
        private readonly ushort _width;

        public HighDefinitionRangeDetailEnhancement(HighDefinitionRangeDetailEnhancementTemplate template)
        {
            if (template.ImageModel.ImageBitDepth != ImageBitDepth.Bpp12)
            {
                throw new ImageModelMismatchException(GetType());
            }
            _imageModel = template.ImageModel;
            _height = _imageModel.Height;
            _width = _imageModel.Width;
            _range = 4096;
            _binaryHistogramThreshold = template.BinaryHistogramThreshold;
            _detailsMinGain = template.DetailsMinGain;
            _detailsMaxGain = template.DetailsMaxGain;
            _bilateralFilter = new BilateralFilterAlgorithm(template.BilateralFilterTemplate);
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            double[] filterWeights;
            var bilateralFiltered = _bilateralFilter.BilateralFiletring(inputImage.ImagePixels, out filterWeights);
            double[] detailsPixels;
            var baseImage = RemovalOfGradientReversalArtifacts(inputImage, bilateralFiltered, filterWeights, out detailsPixels);
            var histogramProjectedImage = BinaryHistogramProjection(baseImage.ToImage());
            var detailsImage = AdaptiveGainControlForDetailedImage(detailsPixels, filterWeights);

            return histogramProjectedImage + detailsImage;
        }

        private ProcessedImage RemovalOfGradientReversalArtifacts(Image referenceImage,
                                                                  Image bilateralFilteredImage,
                                                                  IReadOnlyList<double> filterWeights,
                                                                  out double[] detailsImage)
        {
            detailsImage = new double[_width * _height];
            var baseImage = new double[_width * _height];

            for (var i = 0; i < _width * _height; i++)
            {
                var referencePixel = referenceImage.ImagePixels[i];
                var filteredPixel = bilateralFilteredImage.ImagePixels[i];

                var difference = filterWeights[i] * (referencePixel - filteredPixel);
                var basePixel = filteredPixel + difference;

                baseImage[i] = basePixel;
                detailsImage[i] = referencePixel - basePixel;
            }

            return new ProcessedImage(baseImage, referenceImage.ImageModel);
        }

        public Image BinaryHistogramProjection(Image inputImage)
        {
            var histogram12Bit = new uint[_range];

            for (var i = 0; i < _range; i++)
            {
                histogram12Bit[i] = 0;
            }

            for (var i = 0; i < _width * _height; i++)
            {
                try
                {
                    var pixelValue = inputImage.ImagePixels[i];
                    histogram12Bit[pixelValue]++;
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            uint validPixels = 0;
            for (var i = 0; i < _range; i++)
            {
                if (histogram12Bit[i] >= _binaryHistogramThreshold)
                {
                    histogram12Bit[i] = 1;
                    validPixels++;
                }
                else
                {
                    histogram12Bit[i] = 0;
                }
            }

            var mappedHistogram8Bit = new byte[_range];

            mappedHistogram8Bit[0] = 0;
            uint sum = 0;

            for (var i = 1; i < _range; i++)
            {
                sum += histogram12Bit[i - 1];
                mappedHistogram8Bit[i] = (byte)(Math.Min(255, validPixels) * sum / validPixels);
            }

            var outputImagePixels = new byte[_width * _height];

            for (var i = 0; i < _width * _height; i++)
            {
                var inputImagePixel = inputImage.ImagePixels[i];
                outputImagePixels[i] = mappedHistogram8Bit[inputImagePixel];
            }

            return new Image(outputImagePixels, new ImageModel(_width, _height, ImageBitDepth.Bpp8));
        }

        private Image AdaptiveGainControlForDetailedImage(double[] detailsPixels, IReadOnlyList<double> filterWeights)
        {
            var pixels = new byte[_width * _height];
            for (var i = 0; i < _width * _height; i++)
            {
                pixels[i] = (byte)(detailsPixels[i] * (_detailsMinGain + (1 - filterWeights[i]) * (_detailsMaxGain - _detailsMinGain)) / 16);
            }

            return new Image(pixels, new ImageModel(_width, _height, ImageBitDepth.Bpp8));
        }

        public ImageModel GetTransformedImageModel(ImageModel imageModel)
        {
            var width = imageModel.Width;
            var height = imageModel.Height;

            return new ImageModel(width, height, ImageBitDepth.Bpp8);
        }
    }
}