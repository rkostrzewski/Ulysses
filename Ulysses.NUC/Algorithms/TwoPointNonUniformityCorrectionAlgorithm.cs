using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.NUC.Factories.Templates;
using Ulysses.NUC.NonUniformityModels;

namespace Ulysses.NUC.Algorithms
{
    public class TwoPointNonUniformityCorrectionAlgorithm : BaseNonUnifromityCorrectionAlgorithm
    {
        internal TwoPointNonUniformityCorrectionAlgorithm(TwoPointNonUniformityCorrectionAlgorithmTemplate correctionAlgorithmTemplate) : base(correctionAlgorithmTemplate.NonUniformityModel)
        {
        }

        public override Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = NonUniformityModel.GainCoefficients * inputImagePixels + NonUniformityModel.OffsetCoefficients;

            return outputImage;
        }
    }
}