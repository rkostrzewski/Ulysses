using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates
{
    public interface IImageAcquisitionTemplate : IImageProcessingChainElement
    {
        ImageProcessingAlgorithmType AcquisitionMethod { get; }

        ProcessingAlgorithms.Factories.IImageProcessingAlgorithmTemplate Template { get; }
    }
}