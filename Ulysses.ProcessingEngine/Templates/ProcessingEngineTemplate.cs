using JetBrains.Annotations;
using Ulysses.ImageProviders;
using Ulysses.ProcessingAlgorithms;
using Ulysses.ProcessingEngine.Output;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.ProcessingEngine.Templates
{
    public class ProcessingEngineTemplate
    {
        public ProcessingStrategy ProcessingStrategy { get; set; }

        public IImageProvider ImageProvider { get; set; }

        public IImageProcessingChain ImageProcessingChain { get; set; }

        [CanBeNull]
        public IReceiveProcessedImageCommand ReceiveProcessedImageCommand { get; set; }
    }
}