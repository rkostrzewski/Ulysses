using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public abstract class BaseNonUnifromityCorrectionAlgorithm : Core.IImageProcessingAlgorithm
    {
        protected internal BaseNonUnifromityCorrectionAlgorithm(NonUniformityModel nonUniformityModel)
        {
            NonUniformityModel = nonUniformityModel;
        }

        public NonUniformityModel NonUniformityModel { get; private set; }

        public abstract Image ProcessImage(Image inputImagePixels);
    }
}