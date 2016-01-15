using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels.NonUniformityCorrection
{
    public abstract class BaseNonUniformityTemplateViewModel : IImageProcessingAlgorithmTemplateViewModel
    {
        public abstract ImageProcessingAlgorithm Algorithm { get; }

        public abstract IImageProcessingAlgorithmTemplate Template { get; set; }

        public abstract string NonUniformityModelFilePath { get; set; }

        public abstract bool UseNonUniformityModel { get; set; }
    }
}