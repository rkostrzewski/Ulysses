using System;
using System.Linq;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class GammaAdjustmentAlgorithm : IImageProcessingAlgorithm
    {
        private readonly Pixel[] _lookupTable;

        public GammaAdjustmentAlgorithm(ContrastAdjustmentTemplate template)
        {
            double imageDynamicRange;

            switch (template.ImageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    imageDynamicRange = byte.MaxValue;
                    break;
                case ImageBitDepth.Bpp12:
                    imageDynamicRange = 4095;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _lookupTable = Enumerable.Range(0, Pixel.MaxValue).Select(v => (Pixel)(imageDynamicRange * Math.Pow(v / imageDynamicRange, 1.0 / template.AdjustmentValue))).ToArray();
        }

        public Image ProcessImage(Image inputImage)
        {
            var adjustedPixels = inputImage.ImagePixels.Select(p => _lookupTable[p]);
            return new Image(adjustedPixels, inputImage.ImageModel);
        }
    }
}