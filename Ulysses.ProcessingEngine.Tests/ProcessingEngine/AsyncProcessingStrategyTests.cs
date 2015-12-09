using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.ProcessingEngineStrategies;
using Ulysses.Tests.Core;

[assembly: RequiresMTA]

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    [TestFixture]
    public class AsyncProcessingStrategyTests : BaseProcessingEngineStrategyTests
    {
        [Test]
        public async void ShouldSetOutputImageAfterProcessing()
        {
            // Given
            var engine = new AsyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then
            Assert.That(1 <= TimesImageProcessingChainCalled);
            Assert.That(1 <= TimesSetOutputImageCommandCalled);
        }

        [Test]
        public async void ShouldNotProcessImageBeforeAcquiringIt()
        {
            // Given
            var engine = new AsyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 400);

            // Then
            Assert.AreEqual(1, TimesImageAcquisitorCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldNotWaitWithAcquiringNextImageUntilCurrentIsProcessed()
        {
            // Given
            var engine = new AsyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1400);

            // Then
            Assert.AreEqual(3, TimesImageAcquisitorCalled);
        }

        [Test]
        public async void ShouldProcessNextImageAfterCurrentIsProcessed()
        {
            // Given
            var engine = new AsyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2400);

            // Then
            Assert.AreEqual(2, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldStopWorkingAfterNoImageWasObtained()
        {
            // Given
            var imageAcquisitor = new Mock<IImageAcquisitorStrategy>();
            Image image;
            imageAcquisitor.Setup(ia => ia.TryToObtainImage(out image))
                           .Callback(ImageAcquistiorMockWork)
                           .ReturnsInOrder(true, false);


            var engine = new AsyncProcessingStrategy(imageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2000);
            
            // Then
            Assert.That(1 >= TimesImageProcessingChainCalled);
            Assert.AreEqual(1, TimesSetOutputImageCommandCalled);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedEngine()
        {
            // Given
            var imageAcquisitor = new Mock<IImageAcquisitorStrategy>();
            Image image;
            imageAcquisitor.Setup(ia => ia.TryToObtainImage(out image))
                           .Callback(ImageAcquistiorMockWork)
                           .ReturnsInOrder(true, false);


            var engine = new AsyncProcessingStrategy(imageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            // Then
            Assert.Throws<InvalidEngineStateException>(async () => await engine.Stop());
        }

        [Test]
        public async void ShouldNotAllowToStartAlreadyStartedEngine()
        {
            // Given
            var imageAcquisitor = new Mock<IImageAcquisitorStrategy>();
            Image image;
            imageAcquisitor.Setup(ia => ia.TryToObtainImage(out image))
                           .Callback(ImageAcquistiorMockWork)
                           .ReturnsInOrder(true, false);


            var engine = new AsyncProcessingStrategy(imageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When
            var task = engine.Start();

            // Then
            Assert.Throws<InvalidEngineStateException>(() => { engine.Start(); });

            await engine.Stop();
        }
    }
}