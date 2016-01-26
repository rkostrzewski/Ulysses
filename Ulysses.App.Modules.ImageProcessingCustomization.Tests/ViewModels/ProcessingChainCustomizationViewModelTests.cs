﻿using System.Linq;
using Moq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels
{
    [TestFixture]
    public class ProcessingChainCustomizationViewModelTests
    {
        private IProcessingChainBuilderDataStore _dataStore;
        private Mock<IChangeProcessingChainElementCustomizationRegionViewCommand> _changeCustomizationRegionViewCommandMock;
        private IProcessingChainDropHandler _dropHandler;
        private IProcessingChainDragHandler _dragHandler;
        private IUpdateProcessingEngineCommand _updateProcessingEngineCommand;

        [SetUp]
        public void SetUp()
        {
            _dataStore = new ProcessingChainDataStore();
            _changeCustomizationRegionViewCommandMock = new Mock<IChangeProcessingChainElementCustomizationRegionViewCommand>();
            _dropHandler = new Mock<IProcessingChainDropHandler>().Object;
            _dragHandler = new Mock<IProcessingChainDragHandler>().Object;
            _updateProcessingEngineCommand = new Mock<IUpdateProcessingEngineCommand>().Object;
        }

        [Test]
        public void ShouldSetDragDropHandlersWhenCreated()
        {
            // Given
            // When
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _updateProcessingEngineCommand, _dragHandler, _dropHandler);

            // Then
            Assert.AreEqual(_dragHandler, viewModel.DragHandler);
            Assert.AreEqual(_dropHandler, viewModel.DropHandler);
        }

        [Test]
        public void ShouldNotChangeChainElementCustomizationRegionWhenSameItemIsSelected()
        {
            // Given
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _updateProcessingEngineCommand, _dragHandler, _dropHandler);
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
            var viewModel = new ProcessingChainCustomizationViewModel(_dataStore, _changeCustomizationRegionViewCommandMock.Object, _updateProcessingEngineCommand, _dragHandler, _dropHandler);
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