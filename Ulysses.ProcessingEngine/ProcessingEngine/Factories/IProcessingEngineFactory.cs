using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.ProcessingEngine.ProcessingEngine.Factories
{
    public interface IProcessingEngineFactory
    {
        IProcessingEngine CreateInstance(ProcessingStrategy processingStrategy,
                                         IImageAcquisition imageAcquisition,
                                         IImageProcessingChain imageProcessingChain,
                                         IReceiveProcessedImageCommand setOutputImageCommand);
    }
}