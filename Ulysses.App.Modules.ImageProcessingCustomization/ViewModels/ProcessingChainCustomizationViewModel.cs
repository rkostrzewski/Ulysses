using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.Core.Templates;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels
{
    public class ProcessingChainCustomizationViewModel : NotifyPropertyChanged, IProcessingChainCustomizationViewModel
    {
        private readonly IChangeProcessingChainElementCustomizationRegionViewCommand _changeImageProcessingChainElementCustomizationRegionViewCommand;
        private readonly IProcessingChainBuilderDataStore _processingChainDataStore;
        private IProcessingChainElementTemplate _selectedProcessingChainElementTemplate;

        public ProcessingChainCustomizationViewModel(IProcessingChainBuilderDataStore processingChainDataStore,
                                                     IAvailableProcessingChainElements availableProcessingChainElements,
                                                     IChangeProcessingChainElementCustomizationRegionViewCommand changeImageProcessingChainElementCustomizationRegionViewCommand,
                                                     IUpdateProcessingEngineCommand updateProcessingEngineCommand,
                                                     IRemoveItemFromProcessingChainCommand removeItemFromProcessingChainCommand,
                                                     IProcessingChainDragHandler dragHandler,
                                                     IProcessingChainDropHandler dropHandler)
        {
            _processingChainDataStore = processingChainDataStore;
            _changeImageProcessingChainElementCustomizationRegionViewCommand = changeImageProcessingChainElementCustomizationRegionViewCommand;

            UpdateProcessingEngineCommand = updateProcessingEngineCommand;
            RemoveItemFromProcessingChainCommand = removeItemFromProcessingChainCommand;

            DragHandler = dragHandler;
            DropHandler = dropHandler;

            AvailableImageProcessingAlgorithmTemplates = availableProcessingChainElements.ToList();
        }

        public IList<IProcessingChainElementTemplate> AvailableImageProcessingAlgorithmTemplates { get; set; }

        public ObservableCollection<IProcessingChainElementTemplate> ProcessingChainElements => _processingChainDataStore.ProcessingChainTemplate;

        public IProcessingChainElementTemplate SelectedProcessingChainElementTemplate
        {
            get
            {
                return _selectedProcessingChainElementTemplate;
            }
            set
            {
                if (_selectedProcessingChainElementTemplate == value)
                {
                    return;
                }

                _selectedProcessingChainElementTemplate = value;
                _changeImageProcessingChainElementCustomizationRegionViewCommand.Execute(_selectedProcessingChainElementTemplate);
                OnPropertyChanged();
            }
        }

        public IProcessingChainDragHandler DragHandler { get; }

        public IProcessingChainDropHandler DropHandler { get; }

        public IUpdateProcessingEngineCommand UpdateProcessingEngineCommand { get; }

        public IRemoveItemFromProcessingChainCommand RemoveItemFromProcessingChainCommand { get; }

        public bool IsAsyncModeEnabled
        {
            get
            {
                switch (_processingChainDataStore.ProcessingStrategy)
                {
                    case ProcessingStrategy.Async:
                        return true;
                    case ProcessingStrategy.Sync:
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                var newValue = value ? ProcessingStrategy.Async : ProcessingStrategy.Sync;

                if (_processingChainDataStore.ProcessingStrategy == newValue)
                {
                    return;
                }

                _processingChainDataStore.ProcessingStrategy = newValue;
                OnPropertyChanged();
            }
        }
    }
}