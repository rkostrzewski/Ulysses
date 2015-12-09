using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.Content.ImageDisplay.Commands;
using Ulysses.ProcessingEngine;

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
            Task task = null;
            processingEngineMock.Setup(s => s.Start()).Returns(() =>
            {
                task = Task.Run(() => Thread.Sleep(int.MaxValue));
                return task;
            });

            processingEngineMock.Setup(s => s.IsWorking()).Returns(() => task != null && !task.IsCompleted);

            var command = new StartImageProcessingCommand(processingEngineMock.Object);
            
            // When
            command.Execute();

            // Then
            Assert.IsFalse(command.CanExecute());
        }
    }
}