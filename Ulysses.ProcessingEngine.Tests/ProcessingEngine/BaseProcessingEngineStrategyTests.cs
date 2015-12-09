using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    public class BaseProcessingEngineStrategyTests
    {
        protected int TimesImageAcquisitorCalled;
        protected int TimesImageProcessingChainCalled;
        protected int TimesSetOutputImageCommandCalled;

        protected Mock<IImageAcquisitorStrategy> ImageAcquisitor
        {
            get
            {
                var imageAcquisitor = new Mock<IImageAcquisitorStrategy>();
                Image image;
                imageAcquisitor.Setup(ia => ia.TryToObtainImage(out image))
                               .Callback(ImageAcquistiorMockWork)
                               .Returns(true);
                return imageAcquisitor;
            }
        }

        protected Mock<IImageProcessingChain> ImageProcessingChain
        {
            get
            {
                var imageProcessingChain = new Mock<IImageProcessingChain>();
                imageProcessingChain
                    .Setup(ip => ip.ProcessImage(It.IsAny<Image>()))
                    .Callback(ImageProcessingChainMockWork)
                    .Returns<Image>(null);
                return imageProcessingChain;
            }
        }

        [TearDown]
        public void TestTearDown()
        {
            TimesImageAcquisitorCalled = 0;
            TimesImageProcessingChainCalled = 0;
            TimesSetOutputImageCommandCalled = 0;
        }

        [SetUp]
        public void TestSetUp()
        {
            TimesImageAcquisitorCalled = 0;
            TimesImageProcessingChainCalled = 0;
            TimesSetOutputImageCommandCalled = 0;
        }

        protected Mock<ISetOutputImageCommand> SetOutputImageCommand
        {
            get
            {
                var setOutputImageCommand = new Mock<ISetOutputImageCommand>();
                setOutputImageCommand.Setup(ia => ia.SetOuputImageAsync(It.IsAny<Image>()))
                                     .Callback(SetOutputImageCommandMockWork);
                return setOutputImageCommand;
            }
        }

        protected static async Task StartProcessingWaitForSpecifiedTimeAndWaitForEnd(IProcessingStrategy engine, int milliseconds)
        {
            var task = engine.Start();
            Thread.Sleep(milliseconds);
            await engine.Stop();
        }

        protected void ImageProcessingChainMockWork()
        {
            TimesImageProcessingChainCalled++;
            Thread.Sleep(1000);
        }

        protected void ImageAcquistiorMockWork()
        {
            TimesImageAcquisitorCalled++;
            Thread.Sleep(500);
        }

        protected void SetOutputImageCommandMockWork()
        {
            TimesSetOutputImageCommandCalled++;
        }
    }
}