using System;
using System.Linq;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class ConstantRangeNonUniformityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        private readonly double _meanOfOutputImage;
        private readonly double _standardDeviationOfOutputImage;
        private readonly int _amountOfProcessedImagesToStopCalibration;
        private ProcessedImage _meanOfInputImage;
        private ProcessedImage _standardDeviationOfInputImage;
        private ProcessedImage _gainImage;
        private ProcessedImage _offsetImage;
        private int _timesImageWasProcessed;

        public ConstantRangeNonUniformityCorrectionAlgorithm(ConstantRangeNonUniformityCorrectionTemplate template)
        {
            var imageLength = template.ImageModel.Width * template.ImageModel.Height;
            var xMin = template.RangeMinimum;
            var xMax = template.RangeMaximum;

            _amountOfProcessedImagesToStopCalibration = template.AmountOfProcessedImagesToStopCalibration;
            _standardDeviationOfOutputImage = (xMax - xMin) / Math.Sqrt(12);
            _meanOfOutputImage = (xMax + xMin) / 2.0d;

            _meanOfInputImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d), template.ImageModel);
            _standardDeviationOfInputImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d), template.ImageModel);

            _gainImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 1.0d), template.ImageModel);
            _offsetImage = new ProcessedImage(Enumerable.Range(0, imageLength).Select(p => 0.0d), template.ImageModel);

            _timesImageWasProcessed = 0;
        }

        public Image ProcessImage(Image inputImagePixels)
        {
            if (_timesImageWasProcessed > _amountOfProcessedImagesToStopCalibration && _amountOfProcessedImagesToStopCalibration > 0)
            {
                return CorrectImage(inputImagePixels);
            }

            ReCalculateStatistics(inputImagePixels);
            return CorrectImage(inputImagePixels);
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

            var newGainCoefficients  = _gainImage.Values.Zip(_standardDeviationOfInputImage.Values, (g, std) => Math.Abs(std) > 0.05 ? _standardDeviationOfOutputImage / std : g);
            _gainImage = new ProcessedImage(newGainCoefficients, _gainImage.ImageModel);
            _offsetImage = -_meanOfInputImage * _gainImage + _meanOfOutputImage;
        }
    }
}