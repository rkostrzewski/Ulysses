using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.Content.ImageDisplay.ViewModels;
using Ulysses.ProcessingEngine.ProcessingEngine.Factories;

namespace Ulysses.App.Tests.Modules.Content.ImageDisplay.ViewModels
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
            Assert.IsFalse(viewModel.StartImageProcessingCommand.CanExecute());
            Assert.IsFalse(viewModel.StopImageProcessingCommand.CanExecute());
        }
    }
}