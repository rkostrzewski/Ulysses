using System;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageDisplay.Commands;
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
            var command = new StopImageProcessingCommand(processingEngineMock.Object);

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
            var command = new StopImageProcessingCommand(processingEngineMock.Object);

            // When
            // Then
            Assert.IsFalse(command.CanExecute());
            Assert.Throws<InvalidOperationException>(() => command.Execute());
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