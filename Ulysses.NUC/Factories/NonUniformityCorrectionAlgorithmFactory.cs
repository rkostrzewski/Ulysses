using Ulysses.Core;
using Ulysses.NonUniformityCorrection.Algorithms;
using Ulysses.NonUniformityCorrection.Exceptions;
using Ulysses.NonUniformityCorrection.Factories.Templates;

namespace Ulysses.NonUniformityCorrection.Factories
{
    public class NonUniformityCorrectionAlgorithmFactory
    {
        public IImageProcessingAlgorithm CreateAlgorithmImplementation(BaseNonUniformityCorrectionAlgorithmTemplate correctionAlgorithmTemplate)
        {
            switch (correctionAlgorithmTemplate.Algorithm)
            {
                case NonUniformityCorrectionAlgorithm.TwoPointNonUniformityAlgorithm:
                    return new TwoPointNonUniformityCorrectionAlgorithm(correctionAlgorithmTemplate as TwoPointNonUniformityCorrectionAlgorithmTemplate);
                default:
                    throw new NotSupportedNonUniformityCorrectionAlgorithm();
            }
        }
    }
}