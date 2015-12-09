using System;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Modules.Content;
using Ulysses.App.Modules.Content.ImageDisplay.Views;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views;
using Ulysses.App.Modules.ControlPanel.Commands;
using Ulysses.App.Regions;

namespace Ulysses.App.Tests.Modules.ControlPanel.Commands
{
    [TestFixture]
    public class ChangeContentRegionCommandTests
    {
        [Test]
        [TestCase(ContentViews.ImageDisplay, nameof(ImageDisplayView))]
        [TestCase(ContentViews.ImageProcessingCustomization, nameof(ImageProcessingCustomizationView))]
        public void ShouldNavigateToAnotherRegionWhenExecuted(ContentViews contentView, string viewName)
        {
            // Given
            var regionMock = new Mock<IRegion>();
            regionMock.SetupGet(r => r.Name).Returns(ApplicationRegions.ContentRegion.ToString());
            var regionCollection = new TestRegionCollection()
            {
                regionMock.Object
            };

            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);

            IChangeContentRegionCommand command = new ChangeContentRegionViewCommand(regionManagerMock.Object);

            // When

            command.Execute(contentView);

            // Then

            regionMock.Verify(r => r.RequestNavigate(new Uri(viewName, UriKind.RelativeOrAbsolute), It.IsNotNull<Action<NavigationResult>>()), Times.Once);
        }

        [Test]
        public void ShouldThrowExceptionProvidedUnsupportedViewToNavigateTo()
        {
            // Given
            var regionMock = new Mock<IRegion>();
            regionMock.SetupGet(r => r.Name).Returns(ApplicationRegions.ContentRegion.ToString());
            var regionCollection = new TestRegionCollection()
            {
                regionMock.Object
            };

            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);

            IChangeContentRegionCommand command = new ChangeContentRegionViewCommand(regionManagerMock.Object);

            // When

            Assert.Throws<ArgumentOutOfRangeException>(() => command.Execute((ContentViews)3));
        }

        [Test]
        public void ShouldAllowToExecuteCommandWhenContentRegionExists()
        {
            // Given
            var regionMock = new Mock<IRegion>();
            regionMock.SetupGet(r => r.Name).Returns(ApplicationRegions.ContentRegion.ToString());
            var regionCollection = new TestRegionCollection()
            {
                regionMock.Object
            };

            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);

            IChangeContentRegionCommand command = new ChangeContentRegionViewCommand(regionManagerMock.Object);

            // When
            var canExecute = command.CanExecute(ContentViews.ImageDisplay);

            // Then
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void ShouldNotAllowToExecuteCommandWhenContentRegionDoesNotExists()
        {
            // Given
            var regionMock = new Mock<IRegion>();
            regionMock.SetupGet(r => r.Name).Returns(ApplicationRegions.ControlPanelRegion.ToString());
            var regionCollection = new TestRegionCollection()
            {
                regionMock.Object
            };

            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);

            IChangeContentRegionCommand command = new ChangeContentRegionViewCommand(regionManagerMock.Object);

            // When
            var canExecute = command.CanExecute(ContentViews.ImageDisplay);

            // Then
            Assert.IsFalse(canExecute);
        }
    }
}
