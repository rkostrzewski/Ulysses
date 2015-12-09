using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.ImageProcessingChain;
using Ulysses.ProcessingEngine.Output;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.ProcessingEngine.Tests.ProcessingEngine
{
    public class BaseProcessingEngineTests
    {
        protected int TimesImageAcquisitionCalled;
        protected int TimesImageProcessingChainCalled;
        protected int TimesSetOutputImageCommandCalled;

        protected Mock<IImageAcquisition> ImageAcquisition
        {
            get
            {
                var imageAcquisitionMock = new Mock<IImageAcquisition>();
                Image image;
                imageAcquisitionMock.Setup(ia => ia.TryToObtainImage(out image)).Callback(ImageAcquisitionMockWork).Returns(true);
                return imageAcquisitionMock;
            }
        }

        protected Mock<IImageProcessingChain> ImageProcessingChain
        {
            get
            {
                var imageProcessingChain = new Mock<IImageProcessingChain>();
                imageProcessingChain.Setup(ip => ip.ProcessImage(It.IsAny<Image>())).Callback(ImageProcessingChainMockWork).Returns<Image>(null);
                return imageProcessingChain;
            }
        }

        protected Mock<ISetOutputImageCommand> SetOutputImageCommand
        {
            get
            {
                var setOutputImageCommand = new Mock<ISetOutputImageCommand>();
                setOutputImageCommand.Setup(ia => ia.Execute(It.IsAny<Image>())).Callback(SetOutputImageCommandMockWork);
                return setOutputImageCommand;
            }
        }

        [TearDown]
        public void TestTearDown()
        {
            TimesImageAcquisitionCalled = 0;
            TimesImageProcessingChainCalled = 0;
            TimesSetOutputImageCommandCalled = 0;
        }

        [SetUp]
        public void TestSetUp()
        {
            TimesImageAcquisitionCalled = 0;
            TimesImageProcessingChainCalled = 0;
            TimesSetOutputImageCommandCalled = 0;
        }

        protected static async Task StartProcessingWaitForSpecifiedTimeAndWaitForEnd(IProcessingEngine engine, int milliseconds)
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

        protected void ImageAcquisitionMockWork()
        {
            TimesImageAcquisitionCalled++;
            Thread.Sleep(500);
        }

        protected void SetOutputImageCommandMockWork()
        {
            TimesSetOutputImageCommandCalled++;
        }
    }
}