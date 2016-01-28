using System.Linq;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels
{
    [TestFixture]
    public class ProcessingChainCustomizationViewModelTests
    {
        private IAvailableProcessingChainElements _availableProcessingChainElements;
        private Mock<IChangeProcessingChainElementCustomizationRegionViewCommand> _changeCustomizationRegionViewCommandMock;
        private IProcessingChainBuilderDataStore _dataStore;
        private IProcessingChainDragHandler _dragHandler;
        private IProcessingChainDropHandler _dropHandler;
        private IRemoveItemFromProcessingChainCommand _removeItemFromProcessingChainCommand;
        private IUpdateProcessingEngineCommand _updateProcessingEngineCommand;

        [SetUp]
        public void SetUp()
        {
            _dataStore = new ProcessingChainDataStore();
            _changeCustomizationRegionViewCommandMock = new Mock<IChangeProcessingChainElementCustomizationRegionViewCommand>();
            _dropHandler = new Mock<IProcessingChainDropHandler>().Object;
            _dragHandler = new Mock<IProcessingChainDragHandler>().Object;
            _updateProcessingEngineCommand = new Mock<IUpdateProcessingEngineCommand>().Object;
            _removeItemFromProcessingChainCommand = new Mock<IRemoveItemFromProcessingChainCommand>().Object;
            _availableProcessingChainElements = new AvailableProcessingChainElements();
        }

        [Test]
        public void ShouldSetDragDropHandlersWhenCreated()
        {
            // Given
            // When
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore,
                                                                      _availableProcessingChainElements,
                                                                      _changeCustomizationRegionViewCommandMock.Object,
                                                                      _updateProcessingEngineCommand,
                                                                      _removeItemFromProcessingChainCommand,
                                                                      _dragHandler,
                                                                      _dropHandler);

            // Then
            Assert.AreEqual(_dragHandler, viewModel.DragHandler);
            Assert.AreEqual(_dropHandler, viewModel.DropHandler);
        }

        [Test]
        public void ShouldNotChangeChainElementCustomizationRegionWhenSameItemIsSelected()
        {
            // Given
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore,
                                                                      _availableProcessingChainElements,
                                                                      _changeCustomizationRegionViewCommandMock.Object,
                                                                      _updateProcessingEngineCommand,
                                                                      _removeItemFromProcessingChainCommand,
                                                                      _dragHandler,
                                                                      _dropHandler);
            var selectedElement = viewModel.ProcessingChainElements.First();
            viewModel.SelectedProcessingChainElementTemplate = selectedElement;
            _changeCustomizationRegionViewCommandMock.ResetCalls();

            // When
            viewModel.SelectedProcessingChainElementTemplate = selectedElement;

            // Then
            _changeCustomizationRegionViewCommandMock.Verify(i => i.Execute(It.IsAny<IProcessingChainElementTemplate>()), Times.Never);
        }

        [Test]
        public void ShouldChangeChainElementCustomizationRegionWhenNewItemIsSelected()
        {
            // Given
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore,
                                                                      _availableProcessingChainElements,
                                                                      _changeCustomizationRegionViewCommandMock.Object,
                                                                      _updateProcessingEngineCommand,
                                                                      _removeItemFromProcessingChainCommand,
                                                                      _dragHandler,
                                                                      _dropHandler);
            var selectedElement = viewModel.ProcessingChainElements.First();
            viewModel.SelectedProcessingChainElementTemplate = selectedElement;
            _changeCustomizationRegionViewCommandMock.ResetCalls();

            // When
            selectedElement = viewModel.ProcessingChainElements.Skip(1).First();
            viewModel.SelectedProcessingChainElementTemplate = selectedElement;

            // Then
            _changeCustomizationRegionViewCommandMock.Verify(i => i.Execute(selectedElement), Times.Once);
        }
    }
}