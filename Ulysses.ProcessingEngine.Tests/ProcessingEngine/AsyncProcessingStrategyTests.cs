using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.Tests.Core;

[assembly: RequiresMTA]

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    [TestFixture]
    public class AsyncProcessingEngineTests : BaseProcessingEngineTests
    {
        [Test]
        public async void ShouldSetOutputImageAfterProcessing()
        {
            // Given
            var engine = new AsyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1750);

            // Then
            Assert.That(1 <= TimesImageProcessingChainCalled);
            Assert.That(1 <= TimesReceiveProcessedImageCommandCalled);
        }

        [Test]
        public async void ShouldNotProcessImageBeforeAcquiringIt()
        {
            // Given
            var engine = new AsyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 400);

            // Then
            Assert.AreEqual(1, TimesImageAcquisitionCalled);
            Assert.AreEqual(0, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldNotWaitWithAcquiringNextImageUntilCurrentIsProcessed()
        {
            // Given
            var engine = new AsyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 1400);

            // Then
            Assert.AreEqual(3, TimesImageAcquisitionCalled);
        }

        [Test]
        public async void ShouldProcessNextImageAfterCurrentIsProcessed()
        {
            // Given
            var engine = new AsyncProcessingEngine(ImageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2400);

            // Then
            Assert.AreEqual(2, TimesImageProcessingChainCalled);
        }

        [Test]
        public async void ShouldStopWorkingAfterNoImageWasObtained()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).ReturnsInOrder(true, false);


            var engine = new AsyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await StartProcessingWaitForSpecifiedTimeAndWaitForEnd(engine, 2000);

            // Then
            Assert.That(1 >= TimesImageProcessingChainCalled);
            Assert.AreEqual(1, TimesReceiveProcessedImageCommandCalled);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedEngine()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).ReturnsInOrder(true, false);


            var engine = new AsyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

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
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).ReturnsInOrder(true, false);


            var engine = new AsyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            var task = engine.Start();

            // Then
            Assert.Throws<InvalidEngineStateException>(() => { engine.Start(); });
            await task;
        }

        [Test]
        public async void ShouldInformAboutWorkingWhenThreadRunning()
        {
            // Given
            var imageAcquisition = new Mock<IImageAcquisition>();
            Image image;
            imageAcquisition.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(true);


            var engine = new AsyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

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


            var engine = new AsyncProcessingEngine(imageAcquisition.Object, ImageProcessingChain.Object, ReceiveProcessedImageCommand.Object);

            // When
            await engine.Start();

            // Then
            Assert.IsFalse(engine.IsWorking());
        }
    }
}