using System;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageDisplay.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.ImageDisplay.Tests.Commands
{
    [TestFixture]
    public class StartImageProcessingCommandTest
    {
        [Test]
        public void ShouldAllowToStartNotStartedProcessingEngine()
        {
            // Given
            var processingEngineMock = new Mock<IProcessingEngine>();
            var processingServiceMock = new Mock<IProcessingService>();
            processingServiceMock.SetupGet(ps => ps.ProcessingEngine).Returns(processingEngineMock.Object);

            var command = new StartImageProcessingCommand(processingServiceMock.Object);

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
            var processingServiceMock = new Mock<IProcessingService>();
            processingServiceMock.SetupGet(ps => ps.ProcessingEngine).Returns(processingEngineMock.Object);

            var command = new StartImageProcessingCommand(processingServiceMock.Object);

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