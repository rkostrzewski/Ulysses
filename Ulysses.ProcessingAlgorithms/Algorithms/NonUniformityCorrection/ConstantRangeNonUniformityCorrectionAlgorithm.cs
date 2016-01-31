using System;
using System.Linq;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class ConstantRangeNonUniformityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        private readonly int _amountOfProcessedImagesToStopCalibration;
        private readonly ImageModel _imageModel;
        private readonly double _meanOfOutputImage;
        private readonly double _standardDeviationOfOutputImage;
        private ProcessedImage _gainImage;
        private ProcessedImage _meanOfInputImage;
        private ProcessedImage _offsetImage;
        private ProcessedImage _standardDeviationOfInputImage;
        private int _timesImageWasProcessed;

        public ConstantRangeNonUniformityCorrectionAlgorithm(ConstantRangeNonUniformityCorrectionTemplate template)
        {
            _imageModel = template.ImageModel;
            var imageLength = template.ImageModel.Width * template.ImageModel.Height;
            var xMin = template.RangeMinimum;
            var xMax = template.RangeMaximum;

            _amountOfProcessedImagesToStopCalibration = template.AmountOfProcessedImagesToStopCalibration;
            _standardDeviationOfOutputImage = (xMax - xMin) / Math.Sqrt(12);
            _meanOfOutputImage = (xMax + xMin) / 2.0d;

            _meanOfInputImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d).ToArray(), template.ImageModel);
            _standardDeviationOfInputImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d).ToArray(), template.ImageModel);

            _gainImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 1.0d).ToArray(), template.ImageModel);
            _offsetImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d).ToArray(), template.ImageModel);

            _timesImageWasProcessed = 0;
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            if (_timesImageWasProcessed > _amountOfProcessedImagesToStopCalibration && _amountOfProcessedImagesToStopCalibration > 0)
            {
                return CorrectImage(inputImage);
            }

            ReCalculateStatistics(inputImage);
            return CorrectImage(inputImage);
        }

        private Image CorrectImage(Image inputImagePixels)
        {
            return (_gainImage * inputImagePixels + _offsetImage).ToImage();
        }

        private void ReCalculateStatistics(Image inputImagePixels)
        {
            _timesImageWasProcessed++;

            _meanOfInputImage = inputImagePixels + (_timesImageWasProcessed - 1) * _meanOfInputImage;
            _meanOfInputImage = _meanOfInputImage / _timesImageWasProcessed;

            _standardDeviationOfInputImage = (inputImagePixels - _meanOfInputImage).Abs() + (_timesImageWasProcessed - 1) * _standardDeviationOfInputImage;
            _standardDeviationOfInputImage = _standardDeviationOfInputImage / _timesImageWasProcessed;

            var newGainCoefficients = new double[_gainImage.Values.Length];

            for (var i = 0; i < newGainCoefficients.Length; i++)
            {
                var std = _standardDeviationOfInputImage.Values[i];
                newGainCoefficients[i] = Math.Abs(std) > 0.05 ? _standardDeviationOfOutputImage / std : _gainImage.Values[i];
            }

            _gainImage = new ProcessedImage(newGainCoefficients, _gainImage.ImageModel);
            _offsetImage = -_meanOfInputImage * _gainImage + _meanOfOutputImage;
        }
    }
}