using Moq;
using NUnit.Framework;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageDisplay.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.ImageDisplay.Tests.Commands
{
    [TestFixture]
    public class StopImageProcessingCommandTest
    {
        [Test]
        public void ShouldAllowToStopStartedProcessingEngine()
        {
            // Given
            var processingEngineMock = new Mock<IProcessingEngine>();
            processingEngineMock.Setup(s => s.IsWorking()).Returns(true);
            var processingServiceMock = new Mock<IProcessingService>();
            processingServiceMock.SetupGet(ps => ps.ProcessingEngine).Returns(processingEngineMock.Object);

            var command = new StopImageProcessingCommand(processingServiceMock.Object);

            // When
            command.Execute();

            // Then
            processingEngineMock.Verify(e => e.Stop(), Times.Once);
        }

        [Test]
        public void ShouldNotAllowToStopNotStartedProcessingEngine()
        {
            // Given
            var processingEngineMock = new Mock<IProcessingEngine>();
            processingEngineMock.Setup(s => s.IsWorking()).Returns(false);
            var processingServiceMock = new Mock<IProcessingService>();
            processingServiceMock.SetupGet(ps => ps.ProcessingEngine).Returns(processingEngineMock.Object);

            var command = new StopImageProcessingCommand(processingServiceMock.Object);

            // When
            // Then
            Assert.IsFalse(command.CanExecute());
            Assert.Throws<CannotExecuteCommandException>(() => command.Execute());
        }

        [Test]
        public void ShouldNotAllowToExecuteIfProcessingEngineIsNull()
        {
            // Given
            var command = new StopImageProcessingCommand(null);

            // When
            // Then
            Assert.IsFalse(command.CanExecute());
        }
    }
}