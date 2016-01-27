using System;
using Moq;
using NUnit.Framework;
using Ulysses.App.Core.Tests.Helpers;

namespace Ulysses.App.Core.Tests.Commands
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void ShouldCallInheritedCommandWhenUsingExecuteObject()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestCommand<bool>(invokable.Object);
            var parameter = (object)true;

            // When
            command.Execute(parameter);

            // Then
            invokable.Verify(i => i.Invoke(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ShouldCallInheritedCommandWhenUsingCanExecuteObject()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestCommand<bool>(invokable.Object);
            var parameter = (object)true;

            // When
            command.CanExecute(parameter);

            // Then
            invokable.Verify(i => i.CanInvoke(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ShouldAllowToAddListenerToCanExecuteChanged()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestCommand<bool>(invokable.Object);

            // When
            // Then
            Assert.DoesNotThrow(() => command.CanExecuteChanged += (sender, args) => { });
        }

        [Test]
        public void ShouldAllowToRemoveListenerToCanExecuteChanged()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestCommand<bool>(invokable.Object);
            var listener = new EventHandler((sender, args) => { });
            ;
            command.CanExecuteChanged += listener;

            // When
            // Then
            Assert.DoesNotThrow(() => command.CanExecuteChanged -= listener);
        }
    }
}