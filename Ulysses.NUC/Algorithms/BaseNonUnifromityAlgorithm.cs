using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.NonUniformityCorrection.Algorithms
{
    public abstract class BaseNonUnifromityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        protected internal BaseNonUnifromityCorrectionAlgorithm(NonUniformityModel nonUniformityModel)
        {
            NonUniformityModel = nonUniformityModel;
        }

        public NonUniformityModel NonUniformityModel { get; private set; }

        public abstract Image ProcessImage(Image inputImagePixels);
    }
}