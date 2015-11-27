using System.Collections.Generic;

namespace Ulysses.Core.ImageProcessing
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

        public ImageProcessingChainBuilder Reset()
        {
            _chainSteps.Clear();

            return this;
        }

        public ImageProcessingChain Build()
        {
            return new ImageProcessingChain(_chainSteps);
        }
    }
}