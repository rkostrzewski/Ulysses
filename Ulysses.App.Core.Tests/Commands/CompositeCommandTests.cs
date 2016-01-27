using Moq;
using NUnit.Framework;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Core.Tests.Helpers;

namespace Ulysses.App.Core.Tests.Commands
{
    [TestFixture]
    public class CompositeCommandTests
    {
        [Test]
        [TestCase(true, true)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void ShouldAllowToExecuteOnlyWhenAllRegisteredCommandsCanBeExecuted(bool canFirstCommandBeExecuted, bool canSecondCommandBeExecuted)
        {
            // Given
            var firstCommandInvokable = new Mock<IInvokable<bool>>();
            firstCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(canFirstCommandBeExecuted);
            var firstCommand = new TestCommand<bool>(firstCommandInvokable.Object);

            var secondCommandInvokable = new Mock<IInvokable<bool>>();
            secondCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(canSecondCommandBeExecuted);
            var secondCommand = new TestCommand<bool>(secondCommandInvokable.Object);

            var compositeCommand = new TestCompositeCommand<bool>();
            compositeCommand.RegisterCommand(firstCommand);
            compositeCommand.RegisterCommand(secondCommand);

            // When
            var canExecute = compositeCommand.CanExecute(true);

            // Then
            Assert.AreEqual(canFirstCommandBeExecuted && canSecondCommandBeExecuted, canExecute);
        }

        [Test]
        public void ShouldExecuteAllRegisteredCommands()
        {
            // Given
            var firstCommandInvokable = new Mock<IInvokable<bool>>();
            firstCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(true);
            var firstCommand = new TestCommand<bool>(firstCommandInvokable.Object);

            var secondCommandInvokable = new Mock<IInvokable<bool>>();
            secondCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(true);
            var secondCommand = new TestCommand<bool>(secondCommandInvokable.Object);

            var compositeCommand = new TestCompositeCommand<bool>();
            compositeCommand.RegisterCommand(firstCommand);
            compositeCommand.RegisterCommand(secondCommand);

            // When
            compositeCommand.Execute(true);

            // Then
            firstCommandInvokable.Verify(i => i.Invoke(It.IsAny<bool>()), Times.Once);
            secondCommandInvokable.Verify(i => i.Invoke(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ShouldNotAllowToExecuteWhenNotAllRegisteredCommandsCanBeExecuted()
        {
            // Given
            var firstCommandInvokable = new Mock<IInvokable<bool>>();
            firstCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(false);
            var firstCommand = new TestCommand<bool>(firstCommandInvokable.Object);

            var secondCommandInvokable = new Mock<IInvokable<bool>>();
            secondCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(true);
            var secondCommand = new TestCommand<bool>(secondCommandInvokable.Object);

            var compositeCommand = new TestCompositeCommand<bool>();
            compositeCommand.RegisterCommand(firstCommand);
            compositeCommand.RegisterCommand(secondCommand);

            // When
            // Then
            Assert.Throws<CannotExecuteCommandException>(() => compositeCommand.Execute(true));
        }

        [Test]
        public void ShouldAllowToUnRegisterCommands()
        {
            // Given
            var firstCommandInvokable = new Mock<IInvokable<bool>>();
            firstCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(true);
            var firstCommand = new TestCommand<bool>(firstCommandInvokable.Object);

            var secondCommandInvokable = new Mock<IInvokable<bool>>();
            secondCommandInvokable.Setup(i => i.CanInvoke(It.IsAny<bool>())).Returns(true);
            var secondCommand = new TestCommand<bool>(secondCommandInvokable.Object);

            var compositeCommand = new TestCompositeCommand<bool>();
            compositeCommand.RegisterCommand(firstCommand);
            compositeCommand.RegisterCommand(secondCommand);

            // When
            compositeCommand.UnregisterCommand(firstCommand);
            compositeCommand.UnregisterCommand(secondCommand);

            // Then
            Assert.AreEqual(0, compositeCommand.RegisteredCommands.Count);
        }
    }
}