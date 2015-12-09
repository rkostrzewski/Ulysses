using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.NUC.NonUniformityModels;

namespace Ulysses.NUC.Algorithms
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