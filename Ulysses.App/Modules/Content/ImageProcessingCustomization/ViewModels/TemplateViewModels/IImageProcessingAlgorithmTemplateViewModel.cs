using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels
{
    public interface IImageProcessingAlgorithmTemplateViewModel
    {
        ImageProcessingAlgorithm Algorithm { get; }

        IImageProcessingAlgorithmTemplate Template { get; }
    }
}