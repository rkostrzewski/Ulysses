using System;
using System.Linq;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class ContrastAdjustmentAlgorithm : IImageProcessingAlgorithm
    {
        private readonly Pixel[] _lookupTable;

        public ContrastAdjustmentAlgorithm(ContrastAdjustmentTemplate template)
        {
            int imageDynamicRange;

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

            _lookupTable = Enumerable.Range(0, Pixel.MaxValue).Select(v => (Pixel)(template.AdjustmentValue * (v - imageDynamicRange / 2.0) + imageDynamicRange / 2.0)).ToArray();
        }

        public Image ProcessImage(Image inputImage)
        {
            var adjustedPixels = inputImage.ImagePixels.Select(p => _lookupTable[p]);
            return new Image(adjustedPixels, inputImage.ImageModel);
        }
    }
}