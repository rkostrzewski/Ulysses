using System.ComponentModel;
using NUnit.Framework;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Models;

namespace Ulysses.App.Modules.Navigation.Tests.Models
{
    [TestFixture]
    public class NavigationPanelStateTests
    {
        [Test]
        public void ShouldTriggerPropertyChangedEventWhenCurrentContentRegionViewIsSet()
        {
            // Given
            var navigationPanelState = new NavigationPanelState();
            var timesEventFired = 0;
            object sender = null;
            PropertyChangedEventArgs args = null;

            navigationPanelState.PropertyChanged += (s, a) =>
            {
                sender = s;
                args = a;
                timesEventFired++;
            };

            // When
            navigationPanelState.CurrentContentRegionView = ContentRegionView.ImageProcessingCustomizationView;
            
            // Then
            Assert.AreEqual(1, timesEventFired);
            Assert.AreEqual(navigationPanelState, sender);
            Assert.AreEqual(nameof(NavigationPanelState.CurrentContentRegionView), args.PropertyName);
        }

        [Test]
        public void ShouldNotTriggerPropertyChangedEventWhenCurrentContentRegionViewIsSetToOldValue()
        {
            // Given
            var navigationPanelState = new NavigationPanelState();
            var timesEventFired = 0;
            navigationPanelState.CurrentContentRegionView = ContentRegionView.ImageDisplayView;

            navigationPanelState.PropertyChanged += (s, a) =>
            {
                timesEventFired++;
            };

            // When
            navigationPanelState.CurrentContentRegionView = ContentRegionView.ImageDisplayView;

            // Then
            Assert.AreEqual(0, timesEventFired);
        }

        [Test]
        public void ShouldReturnCurrentContentRegionView()
        {
            // Given
            var navigationPanelState = new NavigationPanelState { CurrentContentRegionView = ContentRegionView.ImageProcessingCustomizationView };

            // When
            // Then
            Assert.AreEqual(ContentRegionView.ImageProcessingCustomizationView, navigationPanelState.CurrentContentRegionView);
        }
    }
}
