using System;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Utils.Commands.Region;

namespace Ulysses.App.Tests.Utils.Commands
{
    [TestFixture]
    public class ChangeRegionViewCommandCommandTests
    {
        private string _regionName;
        private string _viewName;
        private Mock<IRegion> _regionMock;
        private Mock<IRegionManager> _regionManagerMock;

        [SetUp]
        public void SetUp()
        {
            _regionName = "Test Region";
            _viewName = nameof(TestView);
            _regionMock = new Mock<IRegion>();
            _regionMock.SetupGet(r => r.Name).Returns(_regionName);
            _regionMock.SetupGet(r => r.Views).Returns(new TestViewsCollection());

            var regionCollection = new TestRegionCollection { _regionMock.Object };

            _regionManagerMock = new Mock<IRegionManager>();
            _regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);
        }

        [Test]
        public void ShouldAllowToNavigateToAnotherViewWhenExecutedWhenRegionAndViewAreValid()
        {
            // Given            

            IChangeRegionViewCommand<string> command = new ChangeRegionViewCommand<string>(_regionManagerMock.Object, _regionName);

            // When

            command.Execute(_viewName);

            // Then

            _regionMock.Verify(r => r.RequestNavigate(new Uri(_viewName, UriKind.RelativeOrAbsolute), It.IsNotNull<Action<NavigationResult>>()), Times.Once);
        }

        [Test]
        public void ShouldNotAllowToChangeRegionsViewProvidedViewNotRegisteredWithRegionToNavigateTo()
        {
            // Given

            var regionCollection = new TestRegionCollection();
            var regionManagerMock = new Mock<IRegionManager>();
            var notExistingViewName = Guid.NewGuid().ToString();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);
            IChangeRegionViewCommand<string> command = new ChangeRegionViewCommand<string>(regionManagerMock.Object, _regionName);

            // When
            // Then

            Assert.IsFalse(command.CanExecute(notExistingViewName));
            Assert.Throws<InvalidOperationException>(() => command.Execute(notExistingViewName));
        }

        [Test]
        public void ShouldNotAllowToChangeRegionsViewProvidedNotRegisteredRegionToNavigate()
        {
            // Given

            var regionCollection = new TestRegionCollection();
            var regionManagerMock = new Mock<IRegionManager>();
            var notExistingRegionName = Guid.NewGuid().ToString();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);
            IChangeRegionViewCommand<string> command = new ChangeRegionViewCommand<string>(regionManagerMock.Object, notExistingRegionName);

            // When
            // Then

            Assert.IsFalse(command.CanExecute(_viewName));
            Assert.Throws<InvalidOperationException>(() => command.Execute(_viewName));
        }
    }
}