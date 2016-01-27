using Moq;
using NUnit.Framework;
using Prism.Events;
using Prism.Regions;
using Ulysses.App.Modules.Navigation.Commands;

namespace Ulysses.App.Modules.Navigation.Tests.Commands
{
    [TestFixture]
    public class ChangeContentRegionsViewCommandTests
    {
        [Test]
        public void ShouldAllowToCreateCommand()
        {
            // Given
            var regionManager = new Mock<IRegionManager>();
            var eventAggregator = new Mock<IEventAggregator>();
            var command = new ChangeContentRegionsViewCommand(regionManager.Object, eventAggregator.Object);

            // When
            // Then
            Assert.IsNotNull(command);
        }
    }
}