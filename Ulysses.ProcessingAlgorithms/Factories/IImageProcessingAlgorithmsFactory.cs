using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public interface IImageProcessingAlgorithmsFactory
    {
        IImageProcessingAlgorithm CreateInstance(IImageProcessingAlgorithmTemplate template);
    }
}