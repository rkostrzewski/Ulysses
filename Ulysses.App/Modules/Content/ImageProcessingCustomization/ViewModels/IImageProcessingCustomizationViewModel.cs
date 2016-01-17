using System.Collections.ObjectModel;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels
{
    public interface IImageProcessingCustomizationViewModel
    {
        ObservableCollection<IImageProcessingAlgorithmTemplateViewModel> SelectedImageProcessingAlgorithmTemplate { get; set; }
    }
}