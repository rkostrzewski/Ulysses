using System.ComponentModel;
using Moq;
using NUnit.Framework;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.Models;
using Ulysses.App.Modules.Navigation.ViewModels;

namespace Ulysses.App.Modules.Navigation.Tests.ViewModels
{
    [TestFixture]
    public class NavigationPanelViewModelTests
    {
        [Test]
        public void ShouldReturnNavigationPanelStatesCurrentContentRegionView()
        {
            // Given
            var navigationPanelState = new Mock<INavigationPanelState>();
            var changeContentRegionCompositeCommand = new Mock<IChangeContentRegionCompositeCommand>();

            navigationPanelState.SetupGet(n => n.CurrentContentRegionView).Returns(ContentRegionView.ImageDisplayView);

            var viewModel = new NavigationPanelViewModel(navigationPanelState.Object, changeContentRegionCompositeCommand.Object);

            // When
            var currentContentRegionView = viewModel.CurrentContentRegionView;

            // Then
            Assert.AreEqual(ContentRegionView.ImageDisplayView, currentContentRegionView);
        }

        [Test]
        public void ShouldFirePropertyChangedEventOnViewModelWhenNavigationPanelStateIsChanged()
        {
            // Given
            var navigationPanelState = new NavigationPanelState();
            var changeContentRegionCompositeCommand = new Mock<IChangeContentRegionCompositeCommand>();
            var viewModel = new NavigationPanelViewModel(navigationPanelState, changeContentRegionCompositeCommand.Object);
            var timesEventFired = 0;
            object sender = null;
            PropertyChangedEventArgs args = null;
            viewModel.PropertyChanged += (s, a) =>
            {
                timesEventFired++;
                sender = s;
                args = a;
            };

            // When
            navigationPanelState.CurrentContentRegionView = ContentRegionView.ImageProcessingCustomizationView;

            // Then
            Assert.AreEqual(1, timesEventFired);
            Assert.AreEqual(viewModel, sender);
            Assert.AreEqual(args.PropertyName, nameof(NavigationPanelViewModel.CurrentContentRegionView));
        }
    }
}