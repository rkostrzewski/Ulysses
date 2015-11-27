using Ulysses.Core;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.NUC.NonUniformityModels;

namespace Ulysses.NUC.Algorithms
{
    public abstract class BaseNonUnifromityCorrectionAlgorithm : IImageProcessingAlgorithm
    {
        public NonUniformityModel NonUniformityModel { get; private set; }

        protected BaseNonUnifromityCorrectionAlgorithm(NonUniformityModel nonUniformityModel)
        {
            NonUniformityModel = nonUniformityModel;
        }

        public abstract Image ProcessImage(Image inputImagePixels);
    }
}