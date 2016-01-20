using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageProviders;
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
            var engine = new SyncProcessingEngine(ImageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);
            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1);

            // Then

            Assert.AreEqual(1, TimesImageProviderCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldWaitWithAcquiringNextImageUntilCurrentImageIsProcessed()
        {
            // Given
            var engine = new SyncProcessingEngine(ImageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then
            Assert.AreEqual(2, TimesImageProviderCalled);
            Assert.AreEqual(1, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldSetOutputImageAfterProcessing()
        {
            // Given
            var engine = new SyncProcessingEngine(ImageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then

            Assert.AreEqual(1, TimesImageProcessingChainCalled);
            Assert.AreEqual(1, TimesReceiveProcessedImageCommandCalled);
        }

        [Test]
        public async void ShouldStopWorkingAfterNoImageWasObtained()
        {
            // Given
            Image image;
            var imageProvider = new Mock<IImageProvider>();

            imageProvider.Setup(i => i.TryToObtainImage(out image)).Callback(ImageProviderMockWork).Returns(false);

            var engine = new SyncProcessingEngine(imageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When

            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2500);

            // Then

            Assert.AreEqual(1, TimesImageProviderCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedEngine()
        {
            // Given
            var imageProvider = new Mock<IImageProvider>();
            Image image;
            imageProvider.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageProviderMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            // Then
            Assert.Throws<InvalidEngineStateException>(async () => await engine.Stop());
        }

        [Test]
        public async void ShouldNotAllowToStartAlreadyStartedEngine()
        {
            // Given
            var imageProvider = new Mock<IImageProvider>();
            Image image;
            imageProvider.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageProviderMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

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
            var imageProvider = new Mock<IImageProvider>();
            Image image;
            imageProvider.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageProviderMockWork).Returns(true);


            var engine = new SyncProcessingEngine(imageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

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
            var imageProvider = new Mock<IImageProvider>();
            Image image;
            imageProvider.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageProviderMockWork).ReturnsInOrder(true, false);


            var engine = new SyncProcessingEngine(imageProvider.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await engine.Start();

            // Then
            Assert.IsFalse(engine.IsWorking());
        }
    }
}