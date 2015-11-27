using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
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
    }
}