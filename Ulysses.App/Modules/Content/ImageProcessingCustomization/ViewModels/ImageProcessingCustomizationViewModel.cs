using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;
using Ulysses.App.Utils.ViewModels;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels
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

            AvailableImageProcessingAlgorithmTemplates = new List<IImageProcessingAlgorithmTemplate> { new TwoPointNonUniformityTemplate() };
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