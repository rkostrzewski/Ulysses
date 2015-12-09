using System;
using Moq;
using NUnit.Framework;
using Ulysses.ImageAcquisition;
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
            var imageAcquisition = new Mock<IImageAcquisition>().Object;
            var imageProcessingChain = new Mock<IImageProcessingChain>().Object;
            var setOutputImageCommand = new Mock<ISetOutputImageCommand>().Object;
            var factory = new ProcessingEngineFactory();

            // When
            var engine = factory.CreateInstance(processingStrategy, imageAcquisition, imageProcessingChain, setOutputImageCommand);

            // Then
            Assert.IsNotNull(engine);
        }

        [Test]
        public void ShouldThrowWhenProvidedUnsupportedProcessingStrategy()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>().Object;
            var imageProcessingChain = new Mock<IImageProcessingChain>().Object;
            var setOutputImageCommand = new Mock<ISetOutputImageCommand>().Object;
            var factory = new ProcessingEngineFactory();
            var processingStrategy = (ProcessingStrategy)3;

            // When
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateInstance(processingStrategy, imageAcquisition, imageProcessingChain, setOutputImageCommand));
        }
    }
}