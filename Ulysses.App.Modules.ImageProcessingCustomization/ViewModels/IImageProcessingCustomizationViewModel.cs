using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels
{
    public interface IImageProcessingCustomizationViewModel
    {
        IList<IImageProcessingAlgorithmTemplate> AvailableImageProcessingAlgorithmTemplates { get; set; }

        ObservableCollection<IImageProcessingChainElement> ImageProcessingChainElements { get; }

        IImageProcessingChainElement SelectedProcessingChainElement { get; set; }

        IImageProcessingChainDragHandler DragHandler { get; }

        IImageProcessingChainDropHandler DropHandler { get; }
    }
}