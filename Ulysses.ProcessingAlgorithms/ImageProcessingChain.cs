using System.Collections.Generic;
using System.Linq;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms
{
    public class ImageProcessingChain : IImageProcessingChain
    {
        private readonly IEnumerable<IImageProcessingAlgorithm> _imageProcessingAlgorithms;

        public ImageProcessingChain(IEnumerable<IImageProcessingAlgorithm> imageProcessingAlgorithms)
        {
            _imageProcessingAlgorithms = imageProcessingAlgorithms;
        }

        public Image ProcessImage(Image image)
        {
            return _imageProcessingAlgorithms.Aggregate(image, (current, algorithm) => algorithm.ProcessImage(current));
        }
    }
}
