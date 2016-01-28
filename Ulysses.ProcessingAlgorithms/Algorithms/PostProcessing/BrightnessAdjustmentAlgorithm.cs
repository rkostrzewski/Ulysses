using System;
using System.Linq;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class BrightnessAdjustmentAlgorithm : IImageProcessingAlgorithm
    {
        private readonly Pixel[] _lookupTable;

        public BrightnessAdjustmentAlgorithm(BrightnessAdjustmentTemplate template)
        {
            _lookupTable = Enumerable.Range(0, Pixel.MaxValue).Select(v => (Pixel)(v + template.AdjustmentValue)).ToArray();
        }

        public Image ProcessImage(Image inputImage)
        {
            var adjustedPixels = inputImage.ImagePixels.Select(p => _lookupTable[p]);
            return new Image(adjustedPixels, inputImage.ImageModel);
        }
    }
}