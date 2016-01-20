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
            INavigationPanelViewModel viewModel = new NavigationPanelViewModel(null, null, null);

            // When
            var command = viewModel.ChangeContentRegionsViewCommand;

            // Then
            Assert.IsInstanceOf<IChangeContentRegionsViewCommand>(command);
        }
    }
}