using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection
{
    public abstract class BaseNonUniformityCorrectionTemplate : BaseImageProcessingChainElement, IImageProcessingAlgorithmTemplate
    {
        public abstract ImageProcessingAlgorithmType Algorithm { get; }

        public ImageProcessingAlgorithmGroup Group => ImageProcessingAlgorithmGroup.NonUniformityCorrection;

        public abstract ProcessingAlgorithms.Factories.IImageProcessingAlgorithmTemplate Template { get; set; }

        public abstract string NonUniformityModelFilePath { get; set; }

        public abstract bool UseNonUniformityModel { get; set; }
        public string ElementName => Algorithm.ToString();
    }
}