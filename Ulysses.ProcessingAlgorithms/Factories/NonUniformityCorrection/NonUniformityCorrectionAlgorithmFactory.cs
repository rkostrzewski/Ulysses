using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Exceptions;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection
{
    public class NonUniformityCorrectionAlgorithmFactory
    {
        public Core.IImageProcessingAlgorithm CreateAlgorithmImplementation(BaseNonUniformityCorrectionAlgorithmTemplate correctionAlgorithmTemplate)
        {
            switch (correctionAlgorithmTemplate.Algorithm)
            {
                case ImageProcessingAlgorithm.TwoPointNonUniformityAlgorithm:
                    return new TwoPointNonUniformityCorrectionAlgorithm(correctionAlgorithmTemplate as TwoPointNonUniformityCorrectionAlgorithmTemplate);
                default:
                    throw new NotSupportedNonUniformityCorrectionAlgorithm();
            }
        }
    }
}