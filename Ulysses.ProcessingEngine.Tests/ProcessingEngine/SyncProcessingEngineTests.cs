using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.Tests.Core;

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    [TestFixture]
    public class SyncProcessingEngineTests : BaseProcessingEngineTests
    {
        [Test]
        public async void ShouldNotProcessImageBeforeAcquiringIt()
        {
            // Given
            var engine = new SyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);
            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1);

            // Then

            Assert.AreEqual(1, TimesImageAcquisitionCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldWaitWithAcquiringNextImageUntilCurrentImageIsProcessed()
        {
            // Given
            var engine = new SyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then
            Assert.AreEqual(2, TimesImageAcquisitionCalled);
            Assert.AreEqual(1, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldSetOutputImageAfterProcessing()
        {
            // Given
            var engine = new SyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

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
            var imageAcquisition = new Mock<IImageAcquisition>();

            imageAcquisition.Setup(i => i.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(false);

            var engine = new SyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2500);

            // Then

            Assert.AreEqual(1, TimesImageAcquisitionCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedEngine()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When
            // Then
            Assert.Throws<InvalidEngineStateException>(async () => await engine.Stop());
        }

        [Test]
        public async void ShouldNotAllowToStartAlreadyStartedEngine()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When
            var task = engine.Start();

            // Then
            Assert.Throws<InvalidEngineStateException>(() => { engine.Start(); });

            await engine.Stop();
        }

        [Test]
        public async void ShouldInformAboutWorkingWhenThreadRunning()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When
            var task = engine.Start();

            // Then
            Assert.IsTrue(engine.IsWorking());
            await engine.Stop();
        }

        [Test]
        public async void ShouldInformAboutNotWorkingWhenNoThreadRunning()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).ReturnsInOrder(true, false);


            var engine = new SyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, SetOutputImageCommand.Object);

            // When
            await engine.Start();

            // Then
            Assert.IsFalse(engine.IsWorking());
        }
    }
}