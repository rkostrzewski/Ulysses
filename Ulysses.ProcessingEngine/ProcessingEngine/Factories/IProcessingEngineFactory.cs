using Ulysses.ImageProviders;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.ProcessingEngine.ProcessingEngine.Factories
{
    public interface IProcessingEngineFactory
    {
        IProcessingEngine CreateInstance(ProcessingStrategy processingStrategy,
                                         IImageProvider imageProvider,
                                         IImageProcessingChain imageProcessingChain,
                                         IReceiveProcessedImageCommand setOutputImageCommand);
    }
}