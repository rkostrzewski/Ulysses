using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageDisplay.ViewModels;
using Ulysses.ProcessingEngine.ProcessingEngine.Factories;

namespace Ulysses.App.Modules.ImageDisplay.Tests.ViewModels
{
    [TestFixture]
    public class ImageDisplayViewModelTests
    {
        [Test]
        public void ShouldNotAllowToStartAndStopProcessingEngineWhen()
        {
            // Given
            var processingEngineFactoryMock = new Mock<IProcessingEngineFactory>();
            var viewModel = new ImageDisplayViewModel(processingEngineFactoryMock.Object);

            // When
            // Then
            Assert.IsNull(viewModel.OutputImage);
            Assert.IsFalse(viewModel.StartImageProcessingCommand.CanExecute());
            Assert.IsFalse(viewModel.StopImageProcessingCommand.CanExecute());
        }
    }
}