using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Modules.ControlPanel.Commands;
using Ulysses.App.Modules.ControlPanel.ViewModels;

namespace Ulysses.App.Tests.Modules.ControlPanel.ViewModels
{
    [TestFixture]
    public class ControlPanelViewModelTests
    {
        [Test]
        public void ShouldAllowToChangeActiveContentView()
        {
            // Given
            var regionManagerMock = new Mock<IRegionManager>();
            IControlPanelViewModel viewModel = new ControlPanelViewModel(regionManagerMock.Object);

            // When
            var command = viewModel.ChangeContentRegionViewCommand;

            // Then
            Assert.IsInstanceOf<IChangeContentRegionCommand>(command);
        }
    }
}