using System;
using System.Linq;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public class ContrastAdjustmentAlgorithm : IImageProcessingAlgorithm
    {
        private readonly Pixel[] _lookupTable;
        private readonly ImageModel _imageModel;

        public ContrastAdjustmentAlgorithm(ContrastAdjustmentTemplate template)
        {
            _imageModel = template.ImageModel;
            int imageDynamicRange;

            switch (_imageModel.ImageBitDepth)
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