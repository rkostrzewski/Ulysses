using System;
using System.Linq;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class BrightnessAdjustmentAlgorithm : IImageProcessingAlgorithm
    {
        private readonly Pixel[] _lookupTable;
        private readonly ImageModel _imageModel;

        public BrightnessAdjustmentAlgorithm(BrightnessAdjustmentTemplate template)
        {
            _imageModel = template.ImageModel;
            _lookupTable = Enumerable.Range(0, Pixel.MaxValue).Select(v => (Pixel)(v + template.AdjustmentValue)).ToArray();
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            var outputImage = new Image(inputImage.ImageModel);

            for (var i = 0; i < inputImage.ImagePixels.Length; i++)
            {
                var inputPixel = inputImage.ImagePixels[i];
                outputImage.ImagePixels[i] = _lookupTable[inputPixel];
            }

            return outputImage;
        }
    }
}