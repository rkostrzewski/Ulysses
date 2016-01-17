using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.ViewModels;

namespace Ulysses.App.Tests.Modules.NavigationPanel.ViewModels
{
    [TestFixture]
    public class NavigationPanelViewModelTests
    {
        [Test]
        public void ShouldAllowToChangeActiveContentView()
        {
            // Given
            var regionManagerMock = new Mock<IRegionManager>();
            INavigationPanelViewModel viewModel = new NavigationPanelViewModel(regionManagerMock.Object);

            // When
            var command = viewModel.ChangeContentRegionViewCommand;

            // Then
            Assert.IsInstanceOf<IChangeContentRegionViewCommand>(command);
        }
    }
}