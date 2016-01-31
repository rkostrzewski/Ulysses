using System.Collections.Generic;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;
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
            var imageModel = initialImageModel;
            var preparedAlgorithms = new List<IImageProcessingAlgorithm>();

            foreach (var template in templates)
            {
                var algorithm = _imageProcessingAlgorithmsFactory.CreateInstance(template, imageModel);
                var transformsImageModel = algorithm as ITransformsImageModel;

                if (transformsImageModel != null)
                {
                    imageModel = transformsImageModel.GetTransformedImageModel(imageModel);
                }

                preparedAlgorithms.Add(algorithm);
            }

            return new ImageProcessingChain(preparedAlgorithms);
        }
    }
}