using System;
using Moq;
using NUnit.Framework;
using Ulysses.ImageProviders;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;
using Ulysses.ProcessingEngine.ProcessingEngine.Factories;

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

            // When
            var engine = factory.CreateInstance(processingStrategy, imageProvider, imageProcessingChain, receiveProcessedImageCommand);

            // Then
            Assert.IsNotNull(engine);
        }

        [Test]
        public void ShouldThrowWhenProvidedUnsupportedProcessingStrategy()
        {
            // Given
            var imageProvider = new Mock<IImageProvider>().Object;
            var imageProcessingChain = new Mock<IImageProcessingChain>().Object;
            var receiveProcessedImageCommand = new Mock<IReceiveProcessedImageCommand>().Object;
            var factory = new ProcessingEngineFactory();
            var processingStrategy = (ProcessingStrategy)3;

            // When
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateInstance(processingStrategy, imageProvider, imageProcessingChain, receiveProcessedImageCommand));
        }
    }
}