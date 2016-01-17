using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels
{
    public interface IImageProcessingAlgorithmTemplateViewModel
    {
        ImageProcessingAlgorithmType Algorithm { get; }

        ImageProcessingAlgorithmGroup Group { get; }

        IImageProcessingAlgorithmTemplate Template { get; }
    }
}