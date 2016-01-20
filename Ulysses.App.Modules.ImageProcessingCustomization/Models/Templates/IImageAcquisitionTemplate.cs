using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates
{
    public interface IImageProviderTemplate : IImageProcessingChainElement
    {
        ImageProcessingAlgorithmType AcquisitionMethod { get; }

        ProcessingAlgorithms.Factories.IImageProcessingAlgorithmTemplate Template { get; }
    }
}