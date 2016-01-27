using System;
using Moq;
using NUnit.Framework;
using Ulysses.App.Core.Tests.Helpers;

namespace Ulysses.App.Core.Tests.Commands
{
    [TestFixture]
    public class NoParameterCommandTests
    {
        [Test]
        public void ShouldCallInheritedCommandWhenUsingExecuteObject()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestNoParameterCommand<bool>(invokable.Object);

            // When
            command.Execute(null);

            // Then
            invokable.Verify(i => i.Invoke(), Times.Once);
        }

        [Test]
        public void ShouldCallInheritedCommandWhenUsingCanExecuteObject()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestNoParameterCommand<bool>(invokable.Object);

            // When
            command.CanExecute(null);

            // Then
            invokable.Verify(i => i.CanInvoke(), Times.Once);
        }

        [Test]
        public void ShouldAllowToAddListenerToCanExecuteChanged()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestNoParameterCommand<bool>(invokable.Object);

            // When
            // Then
            Assert.DoesNotThrow(() => command.CanExecuteChanged += (sender, args) => { });
        }

        [Test]
        public void ShouldAllowToRemoveListenerToCanExecuteChanged()
        {
            // Given
            var invokable = new Mock<IInvokable<bool>>();
            var command = new TestNoParameterCommand<bool>(invokable.Object);
            var listener = new EventHandler((sender, args) => { });
            ;
            command.CanExecuteChanged += listener;

            // When
            // Then
            Assert.DoesNotThrow(() => command.CanExecuteChanged -= listener);
        }
    }
}