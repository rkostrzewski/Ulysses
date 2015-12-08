using System;
using Ulysses.Core;
using Ulysses.NUC.Algorithms;
using Ulysses.NUC.Exceptions;
using Ulysses.NUC.Factories.Templates;

namespace Ulysses.NUC.Factories
{
    public class NonUniformityCorrectionAlgorithmFactory
    {
        public IImageProcessingAlgorithm CreateAlgorithmImplementation(BaseNonUniformityCorrectionAlgorithmTemplate correctionAlgorithmTemplate)
        {
            switch (correctionAlgorithmTemplate.Algorithm)
            {
                case NonUniformityCorrectionAlgorithm.TwoPointNonUniformityAlgorithm:
                    return new TwoPointNonUniformityCorrectionCorrectionAlgorithm(correctionAlgorithmTemplate as TwoPointNonUniformityCorrectionAlgorithmTemplate);
                default:
                    throw new NotSupportedNonUniformityCorrectionAlgorithm();
            }
        }
    }
}