using System;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Tests.Helpers;
using Ulysses.App.Tests.Regions;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Commands
{
    [TestFixture]
    public class NavigateToImageProcessingChainElementCustomizationViewCommandTests
    {
        private IImageProcessingChainElement _chainElement;
        private Mock<IRegionManager> _regionManagerMock;
        private Mock<IRegion> _regionMock;
        private string _regionName;
        private TestViewLocator _viewLocator;

        [SetUp]
        public void SetUp()
        {
            _viewLocator = new TestViewLocator();
            _chainElement = new TestChainElement();

            _regionName = "Test Region";
            _regionMock = new Mock<IRegion>();
            _regionMock.SetupGet(r => r.Name).Returns(_regionName);
            _regionMock.SetupGet(r => r.Views).Returns(new TestViewsCollection());

            var regionCollection = new TestRegionCollection { _regionMock.Object };

            _regionManagerMock = new Mock<IRegionManager>();
            _regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);
        }

        [Test]
        public void ShouldNotAllowToNavigateToViewWhenProvidedRegionDoesNotExist()
        {
            // Given

            var regionCollection = new TestRegionCollection();
            var regionManagerMock = new Mock<IRegionManager>();
            var notExistingRegionName = Guid.NewGuid().ToString();
            regionManagerMock.SetupGet(r => r.Regions).Returns(regionCollection);
            var command = new NavigateToImageProcessingChainElementCustomizationViewCommand(regionManagerMock.Object, notExistingRegionName, _viewLocator);

            // When
            // Then

            Assert.IsFalse(command.CanExecute(_chainElement));
            Assert.Throws<InvalidOperationException>(() => command.Execute(_chainElement));
        }

        [Test]
        public void ShouldNotAllowToNavigateToViewWhenProvidedTemplatesViewDoesNotExist()
        {
            // Given
            var templateWithoutDefinedView = new TemplateWithoutDefinedView();
            var command = new NavigateToImageProcessingChainElementCustomizationViewCommand(_regionManagerMock.Object, _regionName, _viewLocator);

            // When
            // Then

            Assert.IsFalse(command.CanExecute(templateWithoutDefinedView));
            Assert.Throws<InvalidOperationException>(() => command.Execute(templateWithoutDefinedView));
        }

        [Test]
        public void ShouldNavigateToViewWithCorrectParametersWhenProvidedTemplatesViewExists()
        {
            // Given
            var command = new NavigateToImageProcessingChainElementCustomizationViewCommand(_regionManagerMock.Object, _regionName, _viewLocator);

            // When
            command.Execute(_chainElement);

            // Then

            var expectedParameters = new NavigationParameters { { nameof(IImageProcessingChainElement.Id), _chainElement.Id } };

            _regionMock.Verify(
                r =>
                r.RequestNavigate(new Uri(typeof (TestView).Name, UriKind.RelativeOrAbsolute),
                                  It.IsNotNull<Action<NavigationResult>>(),
                                  expectedParameters),
                Times.Once);
        }
    }
}