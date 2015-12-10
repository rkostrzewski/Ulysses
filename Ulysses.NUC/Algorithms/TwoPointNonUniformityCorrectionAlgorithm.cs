using Ulysses.Core.Models;
using Ulysses.NonUniformityCorrection.Factories.Templates;

namespace Ulysses.NonUniformityCorrection.Algorithms
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