using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels
{
    public interface IImageAcquisitionTemplateViewModel
    {
        ImageProcessingAlgorithmType AcquisitionMethod { get; }

        IImageProcessingAlgorithmTemplate Template { get; }
    }
}