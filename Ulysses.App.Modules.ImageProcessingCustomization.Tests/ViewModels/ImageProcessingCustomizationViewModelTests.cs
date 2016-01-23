using System.Linq;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels
{
    [TestFixture]
    public class ImageProcessingCustomizationViewModelTests
    {
        private IImageProcessingChainDataStore _dataStore;
        private Mock<IChangeImageProcessingChainElementCustomizationRegionViewCommand> _changeCustomizationRegionViewCommandMock;
        private IImageProcessingChainDropHandler _dropHandler;
        private IImageProcessingChainDragHandler _dragHandler;

        [SetUp]
        public void SetUp()
        {
            _dataStore = new ImageProcessingChainDataStore();
            _changeCustomizationRegionViewCommandMock = new Mock<IChangeImageProcessingChainElementCustomizationRegionViewCommand>();
            _dropHandler = new Mock<IImageProcessingChainDropHandler>().Object;
            _dragHandler = new Mock<IImageProcessingChainDragHandler>().Object;
        }

        [Test]
        public void ShouldSetDragDropHandlersWhenCreated()
        {
            // Given
            // When
            var viewModel = new ImageProcessingCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _dragHandler, _dropHandler);

            // Then
            Assert.AreEqual(_dragHandler, viewModel.DragHandler);
            Assert.AreEqual(_dropHandler, viewModel.DropHandler);
        }

        [Test]
        public void ShouldNotChangeChainElementCustomizationRegionWhenSameItemIsSelected()
        {
            // Given
            var viewModel = new ImageProcessingCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _dragHandler, _dropHandler);
            var selectedElement = viewModel.ImageProcessingChainElements.First();
            viewModel.SelectedProcessingChainElement = selectedElement;
            _changeCustomizationRegionViewCommandMock.ResetCalls();

            // When
            viewModel.SelectedProcessingChainElement = selectedElement;

            // Then
            _changeCustomizationRegionViewCommandMock.Verify(i => i.Execute(It.IsAny<IImageProcessingChainElement>()), Times.Never);
        }

        [Test]
        public void ShouldChangeChainElementCustomizationRegionWhenNewItemIsSelected()
        {
            // Given
            var viewModel = new ImageProcessingCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _dragHandler, _dropHandler);
            var selectedElement = viewModel.ImageProcessingChainElements.First();
            viewModel.SelectedProcessingChainElement = selectedElement;
            _changeCustomizationRegionViewCommandMock.ResetCalls();

            // When
            selectedElement = viewModel.ImageProcessingChainElements.Skip(1).First();
            viewModel.SelectedProcessingChainElement = selectedElement;

            // Then
            _changeCustomizationRegionViewCommandMock.Verify(i => i.Execute(selectedElement), Times.Once);
        }
    }
}