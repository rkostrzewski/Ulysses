using System;
using Ulysses.Core;
using Ulysses.NUC.Algorithms;
using Ulysses.NUC.Exceptions;
using Ulysses.NUC.Factories.Templates;

namespace Ulysses.NUC.Factories
{
    public class NUCAlgorithmFactory
    {
        public IImageProcessingAlgorithm CreateAlgorithmImplementation(BaseNUCAlgorithmTemplate correctionAlgorithmTemplate)
        {
            switch (correctionAlgorithmTemplate.Algorithm)
            {
                case NUCAlgorithm.TwoPointNonUniformityAlgorithm:
                    return new TwoPointNUCAlgorithm(correctionAlgorithmTemplate as TwoPointNUCAlgorithmTemplate);
                default:
                    throw new NotSupportedNUCAlgorithm();
            }
        }
    }
}