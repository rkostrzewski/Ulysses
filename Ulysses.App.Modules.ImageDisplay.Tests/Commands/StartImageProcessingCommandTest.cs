using System;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.Content.ImageDisplay.Commands;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Tests.Modules.Content.ImageDisplay.Commands
{
    [TestFixture]
    public class StartImageProcessingCommandTest
    {
        [Test]
        public void ShouldAllowToStartNotStartedProcessingEngine()
        {
            // Given
            var processingEngineMock = new Mock<IProcessingEngine>();
            var command = new StartImageProcessingCommand(processingEngineMock.Object);

            // When
            command.Execute();

            // Then
            processingEngineMock.Verify(s => s.Start(), Times.Once);
        }

        [Test]
        public void ShouldNotAllowToStartIfAlreadyStarted()
        {
            // Given
            var processingEngineMock = new Mock<IProcessingEngine>();

            processingEngineMock.Setup(s => s.IsWorking()).Returns(true);

            var command = new StartImageProcessingCommand(processingEngineMock.Object);

            // When         
            // Then
            Assert.IsFalse(command.CanExecute());
            Assert.Throws<InvalidOperationException>(command.Execute);
        }

        [Test]
        public void ShouldNotAllowToExecuteIfProcessingEngineIsNull()
        {
            // Given
            var command = new StartImageProcessingCommand(null);

            // When
            // Then
            Assert.IsFalse(command.CanExecute());
        }
    }
}