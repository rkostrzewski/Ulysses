using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.NUC.NonUniformityModels;

namespace Ulysses.NUC.Algorithms
{
    public class TwoPointNonUniformityCorrectionCorrectionAlgorithm : BaseNonUnifromityCorrectionAlgorithm
    {
        public TwoPointNonUniformityCorrectionCorrectionAlgorithm(NonUniformityModel nonUniformityModel) : base(nonUniformityModel)
        {
        }

        public override Image ProcessImage(Image inputImagePixels)
        {
            var outputImage = NonUniformityModel.GainCoefficients * inputImagePixels + NonUniformityModel.OffsetCoefficients;

            return outputImage;
        }
    }
}