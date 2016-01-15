using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionAlgorithm : BaseNonUnifromityCorrectionAlgorithm
    {
        public TwoPointNonUniformityCorrectionAlgorithm(TwoPointNonUniformityCorrectionAlgorithmTemplate correctionAlgorithmTemplate) : base(correctionAlgorithmTemplate.NonUniformityModel)
        {
        }

        public override Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = NonUniformityModel.GainCoefficients * inputImagePixels + NonUniformityModel.OffsetCoefficients;

            return outputImage;
        }
    }
}