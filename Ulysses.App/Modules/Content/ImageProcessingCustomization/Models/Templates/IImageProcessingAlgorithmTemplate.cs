using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates
{
    public interface IImageProcessingAlgorithmTemplate : IImageProcessingChainElement
    {
        ImageProcessingAlgorithmType Algorithm { get; }

        ImageProcessingAlgorithmGroup Group { get; }

        ProcessingAlgorithms.Factories.IImageProcessingAlgorithmTemplate Template { get; }
    }
}