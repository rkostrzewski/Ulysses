﻿using Moq;
using NUnit.Framework;
using Prism.Events;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.Models;

namespace Ulysses.App.Modules.Navigation.Tests.Commands
{
    [TestFixture]
    public class ChangeCurrentRegionInNavigationPanelCommandTests
    {
        [Test]
        public void ShouldNotAllowToExecuteCommandWhenNavigationPanelStateIsNotProvided()
        {
            // Given
            var eventAggregator = new Mock<IEventAggregator>();
            var command = new ChangeCurrentRegionInNavigationPanelCommand(null);

            // When
            // Then
            Assert.IsFalse(command.CanExecute(ContentRegionView.ImageDisplayView));
            Assert.Throws<CannotExecuteCommandException>(() => command.Execute(ContentRegionView.ImageDisplayView));
        }

        [Test]
        public void ShouldChangeCurrentContentRegionViewWhenExecuted()
        {
            // Given
            var eventAggregator = new Mock<IEventAggregator>();
            var navigationPanelState = new Mock<INavigationPanelState>();
            navigationPanelState.SetupSet(n => n.CurrentContentRegionView = It.IsAny<ContentRegionView>()).Verifiable();
            var command = new ChangeCurrentRegionInNavigationPanelCommand(navigationPanelState.Object);

            // When
            command.Execute(ContentRegionView.ImageDisplayView);

            // Then
            navigationPanelState.VerifySet(n => n.CurrentContentRegionView = ContentRegionView.ImageDisplayView, Times.Once);
        }
    }
}