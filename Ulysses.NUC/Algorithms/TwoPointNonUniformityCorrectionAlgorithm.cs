using Ulysses.Core.Models;
using Ulysses.NUC.Factories.Templates;

namespace Ulysses.NUC.Algorithms
{
    public class TwoPointNUCAlgorithm : BaseNonUnifromityCorrectionAlgorithm
    {
        internal TwoPointNUCAlgorithm(TwoPointNUCAlgorithmTemplate correctionAlgorithmTemplate) : base(correctionAlgorithmTemplate.NonUniformityModel)
        {
        }

        public override Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = NonUniformityModel.GainCoefficients * inputImagePixels + NonUniformityModel.OffsetCoefficients;

            return outputImage;
        }
    }
}