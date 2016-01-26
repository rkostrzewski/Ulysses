using System;
using Moq;
using NUnit.Framework;
using Ulysses.ImageProviders;
using Ulysses.ProcessingAlgorithms;
using Ulysses.ProcessingEngine.Factories;
using Ulysses.ProcessingEngine.Output;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine.Factories
{
    [TestFixture]
    public class ProcessingEngineFactoryTests
    {
        [Test]
        [TestCase(ProcessingStrategy.Sync)]
        [TestCase(ProcessingStrategy.Async)]
        public void ShouldCreateInstanceOfProcessingEngine(ProcessingStrategy processingStrategy)
        {
            // Given
            var imageProvider = new Mock<IImageProvider>().Object;
            var imageProcessingChain = new Mock<IImageProcessingChain>().Object;
            var receiveProcessedImageCommand = new Mock<IReceiveProcessedImageCommand>().Object;
            var factory = new ProcessingEngineFactory();
            var template = new ProcessingEngineTemplate
            {
                ProcessingStrategy = processingStrategy,
                ImageProvider = imageProvider,
                ImageProcessingChain = imageProcessingChain,
                ReceiveProcessedImageCommand = receiveProcessedImageCommand
            };

            // When
            var engine = factory.CreateInstance(template);

            // Then
            Assert.IsNotNull(engine);
        }

        [Test]
        public void ShouldThrowWhenProvidedUnsupportedProcessingStrategy()
        {
            // Given
            const ProcessingStrategy processingStrategy = (ProcessingStrategy)3;
            var imageProvider = new Mock<IImageProvider>().Object;
            var imageProcessingChain = new Mock<IImageProcessingChain>().Object;
            var receiveProcessedImageCommand = new Mock<IReceiveProcessedImageCommand>().Object;
            var factory = new ProcessingEngineFactory();
            var template = new ProcessingEngineTemplate
            {
                ProcessingStrategy = processingStrategy,
                ImageProvider = imageProvider,
                ImageProcessingChain = imageProcessingChain,
                ReceiveProcessedImageCommand = receiveProcessedImageCommand
            };

            // When
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateInstance(template));
        }
    }
}