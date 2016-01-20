using System;
using Ulysses.ImageProviders;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.ProcessingEngine.ProcessingEngine.Factories
{
    public class ProcessingEngineFactory : IProcessingEngineFactory
    {
        public IProcessingEngine CreateInstance(ProcessingStrategy processingStrategy,
                                                IImageProvider imageProvider,
                                                IImageProcessingChain imageProcessingChain,
                                                IReceiveProcessedImageCommand setOutputImageCommand)
        {
            switch (processingStrategy)
            {
                case ProcessingStrategy.Sync:
                    return new SyncProcessingEngine(imageProvider, imageProcessingChain, setOutputImageCommand);
                case ProcessingStrategy.Async:
                    return new AsyncProcessingEngine(imageProvider, imageProcessingChain, setOutputImageCommand);
                default:
                    throw new ArgumentOutOfRangeException(nameof(processingStrategy), processingStrategy, null);
            }
        }
    }
}