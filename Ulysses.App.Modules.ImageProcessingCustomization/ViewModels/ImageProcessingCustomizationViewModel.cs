using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels
{
    public class ImageProcessingCustomizationViewModel : NotifyPropertyChanged, IImageProcessingCustomizationViewModel
    {
        private readonly IImageProcessingChainDataStore _imageProcessingChainDataStore;
        private readonly IChangeImageProcessingChainElementCustomizationRegionViewCommand _changeImageProcessingChainElementCustomizationRegionViewCommand;
        private IImageProcessingChainElement _selectedProcessingChainElement;

        public ImageProcessingCustomizationViewModel(IImageProcessingChainDataStore imageProcessingChainDataStore,
                                                     IChangeImageProcessingChainElementCustomizationRegionViewCommand changeImageProcessingChainElementCustomizationRegionViewCommand,
                                                     IImageProcessingChainDragHandler dragHandler,
                                                     IImageProcessingChainDropHandler dropHandler)
        {
            _imageProcessingChainDataStore = imageProcessingChainDataStore;
            _changeImageProcessingChainElementCustomizationRegionViewCommand = changeImageProcessingChainElementCustomizationRegionViewCommand;

            DragHandler = dragHandler;
            DropHandler = dropHandler;

            AvailableImageProcessingAlgorithmTemplates = new List<IImageProcessingAlgorithmTemplate> { new TwoPointNonUniformityCorrectionTemplate() };
        }

        public IList<IImageProcessingAlgorithmTemplate> AvailableImageProcessingAlgorithmTemplates { get; set; }

        public ObservableCollection<IImageProcessingChainElement> ImageProcessingChainElements => _imageProcessingChainDataStore.ImageProcessingChainElements;

        public IImageProcessingChainElement SelectedProcessingChainElement
        {
            get
            {
                return _selectedProcessingChainElement;
            }
            set
            {
                if (_selectedProcessingChainElement == value)
                {
                    return;
                }

                _selectedProcessingChainElement = value;
                _changeImageProcessingChainElementCustomizationRegionViewCommand.Execute(_selectedProcessingChainElement);
                OnPropertyChanged();
            }
        }

        public IImageProcessingChainDragHandler DragHandler { get; }

        public IImageProcessingChainDropHandler DropHandler { get; }
    }
}