using System;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.ProcessingEngine.ProcessingEngine.Factories
{
    public class ProcessingEngineFactory : IProcessingEngineFactory
    {
        public IProcessingEngine CreateInstance(ProcessingStrategy processingStrategy,
                                                IImageAcquisition imageAcquisition,
                                                IImageProcessingChain imageProcessingChain,
                                                IReceiveProcessedImageCommand setOutputImageCommand)
        {
            switch (processingStrategy)
            {
                case ProcessingStrategy.Sync:
                    return new SyncProcessingEngine(imageAcquisition, imageProcessingChain, setOutputImageCommand);
                case ProcessingStrategy.Async:
                    return new AsyncProcessingEngine(imageAcquisition, imageProcessingChain, setOutputImageCommand);
                default:
                    throw new ArgumentOutOfRangeException(nameof(processingStrategy), processingStrategy, null);
            }
        }
    }
}