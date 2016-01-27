using System.Collections.Generic;
using Ulysses.Core.Models;
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

        public IImageProcessingChain BuildChain(IEnumerable<IImageProcessingAlgorithmTemplate> templates, ImageModel initialImageModel)
        {
            //var preparedAlgorithms = templates.Select(template => _imageProcessingAlgorithmsFactory.CreateInstance(template, initialImageModel));
            var preparedAlgorithms = new List<IImageProcessingAlgorithm>();
            foreach (var template in templates)
            {
                preparedAlgorithms.Add(_imageProcessingAlgorithmsFactory.CreateInstance(template, initialImageModel));
            }

            return new ImageProcessingChain(preparedAlgorithms);
        }
    }
}