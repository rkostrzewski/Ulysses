using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.ProcessingEngineStrategies;

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    [TestFixture]
    public class SyncProcessingEngineTests : BaseProcessingEngineStrategyTests
    {
        [Test]
        public async void ShouldNotProcessImageBeforeAcquiringIt()
        {
            // Given
            var engine = new SyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);
            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1);

            // Then

            Assert.AreEqual(1, TimesImageAcquisitorCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldWaitWithAcquiringNextImageUntilCurrentImageIsProcessed()
        {
            // Given
            var engine = new SyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then
            Assert.AreEqual(2, TimesImageAcquisitorCalled);
            Assert.AreEqual(1, TimesImageProcessingChainCalled);
        }
        
        [Test]
        public async void ShouldSetOutputImageAfterProcessing()
        {
            // Given
            var engine = new SyncProcessingStrategy(ImageAcquisitor.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then

            Assert.AreEqual(1, TimesImageProcessingChainCalled);
            Assert.AreEqual(1, TimesSetOutputImageCommandCalled);
        }

        [Test]
        public async void ShouldStopWorkingAfterNoImageWasObtained()
        {
            // Given
            Image image;
            var imageAcquisitorMock = new Mock<IImageAcquisitorStrategy>();
            
            imageAcquisitorMock.Setup(i => i.TryToObtainImage(out image))
                .Callback(ImageAcquistiorMockWork)
                .Returns(false);

            var engine = new SyncProcessingStrategy(imageAcquisitorMock.Object,
                                                     ImageProcessingChain.Object,
                                                     SetOutputImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2500);

            // Then

            Assert.AreEqual(1, TimesImageAcquisitorCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedEngine()
        {
            // Given
            var imageAcquisitor = new Mock<IImageAcquisitorStrategy>();
            Image image;
            imageAcquisitor.Setup(ia => ia.TryToObtainImage(out image))
                           .Callback(ImageAcquistiorMockWork)
                           .Returns(true);


            var engine = new SyncProcessingStrategy(imageAcquisitor.Object,
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
                           .Returns(true);


            var engine = new SyncProcessingStrategy(imageAcquisitor.Object,
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