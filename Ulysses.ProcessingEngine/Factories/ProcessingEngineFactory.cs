using System;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.ProcessingEngine.Factories
{
    public class ProcessingEngineFactory : IProcessingEngineFactory
    {
        public IProcessingEngine CreateInstance(ProcessingEngineTemplate template)
        {
            var imageProvider = template.ImageProvider;
            var imageProcessingChain = template.ImageProcessingChain;
            var receiveProcessedImageCommand = template.ReceiveProcessedImageCommand;
            var processingStrategy = template.ProcessingStrategy;

            switch (processingStrategy)
            {
                case ProcessingStrategy.Sync:
                    return new SyncProcessingEngine(imageProvider, imageProcessingChain, receiveProcessedImageCommand);
                case ProcessingStrategy.Async:
                    return new AsyncProcessingEngine(imageProvider, imageProcessingChain, receiveProcessedImageCommand);
                default:
                    throw new ArgumentOutOfRangeException(nameof(template.ProcessingStrategy));
            }
        }
    }
}