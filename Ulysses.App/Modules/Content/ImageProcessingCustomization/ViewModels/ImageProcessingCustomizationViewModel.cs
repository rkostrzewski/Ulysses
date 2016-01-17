using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels.NonUniformityCorrection;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels
{
    public class ImageProcessingCustomizationViewModel : IImageProcessingCustomizationViewModel
    {
        public ImageProcessingCustomizationViewModel()
        {
            SelectedImageProcessingAlgorithmTemplate = new ObservableCollection<IImageProcessingAlgorithmTemplateViewModel>();
            AvailableImageProcessingAlgorithmTemplates = new List<IImageProcessingAlgorithmTemplateViewModel> { new TwoPointNonUniformityTemplateViewModel() };
        }

        public IList<IImageProcessingAlgorithmTemplateViewModel> AvailableImageProcessingAlgorithmTemplates { get; set; }

        public IImageAcquisitionTemplateViewModel ImageAcquisitionTemplateViewModel { get; set; }

        public ObservableCollection<IImageProcessingAlgorithmTemplateViewModel> SelectedImageProcessingAlgorithmTemplate { get; set; }
    }
}