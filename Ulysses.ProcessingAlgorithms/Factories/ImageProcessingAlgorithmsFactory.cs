using System;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public class ImageProcessingAlgorithmsFactory : IImageProcessingAlgorithmsFactory
    {
        public IImageProcessingAlgorithm CreateInstance(IImageProcessingAlgorithmTemplate template)
        {
            var iImageProcessingAlgorithmTemplateType = typeof (IImageProcessingAlgorithm);
            var algorithmType = template.AlgorithmType;

            if (!(algorithmType.IsClass && !algorithmType.IsClass && iImageProcessingAlgorithmTemplateType.IsAssignableFrom(algorithmType)))
            {
                throw new InvalidOperationException();
            }

            var algorithmConstructor = algorithmType.GetConstructor(new[] { template.GetType() });

            if (algorithmConstructor == null)
            {
                throw new InvalidOperationException();
            }

            var algorithmInstance = algorithmConstructor.Invoke(new object[] { template }) as IImageProcessingAlgorithm;

            if (algorithmInstance == null)
            {
                throw new InvalidOperationException();
            }

            return algorithmInstance;
        }
    }
}
