using System;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Exceptions;
using Ulysses.ProcessingAlgorithms.Templates;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public class ImageProcessingAlgorithmsFactory : IImageProcessingAlgorithmsFactory
    {
        private readonly INonUniformityModelProvider _nonUniformityModelProvider;

        public ImageProcessingAlgorithmsFactory(INonUniformityModelProvider nonUniformityModelProvider)
        {
            _nonUniformityModelProvider = nonUniformityModelProvider;
        }

        public IImageProcessingAlgorithm CreateInstance(IImageProcessingAlgorithmTemplate template, ImageModel imageModel)
        {
            var iImageProcessingAlgorithmTemplateType = typeof (IImageProcessingAlgorithm);
            var algorithmType = template.AlgorithmType;

            if (!(algorithmType.IsClass && !algorithmType.IsAbstract && iImageProcessingAlgorithmTemplateType.IsAssignableFrom(algorithmType)))
            {
                throw new AlgorithmCreationException(algorithmType);
            }

            var nonUniformityCorrectionTemplate = template as BaseNonUniformityCorrectionTemplate;
            if (nonUniformityCorrectionTemplate == null || !nonUniformityCorrectionTemplate.UseNonUniformityModel)
            {
                return CreateAlgorithm(template, algorithmType);
            }

            var nonUniformitySourceFilePath = nonUniformityCorrectionTemplate.NonUniformityModelFilePath;
            nonUniformityCorrectionTemplate.NonUniformityModel = _nonUniformityModelProvider.GetNonUniformityModel(nonUniformitySourceFilePath, imageModel);

            return CreateAlgorithm(nonUniformityCorrectionTemplate, algorithmType);
        }

        private static IImageProcessingAlgorithm CreateAlgorithm(IImageProcessingAlgorithmTemplate template, Type algorithmType)
        {
            var algorithmConstructor = algorithmType.GetConstructor(new[] { template.GetType() });

            if (algorithmConstructor == null)
            {
                throw new AlgorithmCreationException(algorithmType);
            }

            var algorithmInstance = algorithmConstructor.Invoke(new object[] { template }) as IImageProcessingAlgorithm;

            if (algorithmInstance == null)
            {
                throw new AlgorithmCreationException(algorithmType);
            }

            return algorithmInstance;
        }
    }
}