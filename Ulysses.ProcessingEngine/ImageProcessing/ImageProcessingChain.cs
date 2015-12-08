using System.Collections.Generic;
using System.Linq;
using Ulysses.Core;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine.ImageProcessing
{
    public class ImageProcessingChain : IImageProcessingChain
    {
        private readonly IList<IImageProcessingAlgorithm> _chainSteps;
        public ImageProcessingChain(IList<IImageProcessingAlgorithm> chainSteps)
        {
            _chainSteps = chainSteps;
        }

        public Image ProcessImage(Image image)
        {
            return _chainSteps.Aggregate(image, (current, step) => step.ProcessImage(current));
        }
    }
}