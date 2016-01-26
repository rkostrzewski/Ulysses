using System.Collections.Generic;
using System.Linq;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public class ImageProcessingChainFactory : IImageProcessingChainFactory
    {
        private readonly IImageProcessingAlgorithmsFactory _imageProcessingAlgorithmsFactory;

        public ImageProcessingChainFactory(IImageProcessingAlgorithmsFactory imageProcessingAlgorithmsFactory)
        {
            _imageProcessingAlgorithmsFactory = imageProcessingAlgorithmsFactory;
        }

        public IImageProcessingChain BuildChain(IEnumerable<IImageProcessingAlgorithmTemplate> templates)
        {
            var preparedAlgorithms = templates.Select(template => _imageProcessingAlgorithmsFactory.CreateInstance(template));

            return new ImageProcessingChain(preparedAlgorithms);
        }
    }
}
