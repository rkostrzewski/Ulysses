using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionAlgorithm : BaseNonUniformityCorrectionAlgorithm
    {
        public TwoPointNonUniformityCorrectionAlgorithm(TwoPointNonUniformityCorrectionTemplate correctionAlgorithmTemplate)
            : base(correctionAlgorithmTemplate.NonUniformityModel)
        {
        }

        public override Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = NonUniformityModel.GainCoefficients * inputImagePixels + NonUniformityModel.OffsetCoefficients;

            return outputImage;
        }
    }
}