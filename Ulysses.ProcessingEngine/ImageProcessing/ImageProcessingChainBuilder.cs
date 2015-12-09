using System.Collections.Generic;
using Ulysses.Core;

namespace Ulysses.ProcessingEngine.ImageProcessing
{
    public class ImageProcessingChainBuilder
    {
        private readonly IList<IImageProcessingAlgorithm> _chainSteps;

        public ImageProcessingChainBuilder()
        {
            _chainSteps = new List<IImageProcessingAlgorithm>();
        }

        public ImageProcessingChainBuilder AddStepToChain(IImageProcessingAlgorithm correctionAlgorithm)
        {
            _chainSteps.Add(correctionAlgorithm);

            return this;
        }

        public ImageProcessingChain Build()
        {
            return new ImageProcessingChain(_chainSteps);
        }
    }
}