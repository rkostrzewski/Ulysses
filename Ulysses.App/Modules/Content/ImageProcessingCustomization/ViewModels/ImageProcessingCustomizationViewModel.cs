using System.Collections.ObjectModel;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels.NonUniformityCorrection;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels
{
    public class ImageProcessingCustomizationViewModel : IImageProcessingCustomizationViewModel
    {
        public ObservableCollection<IImageProcessingAlgorithmTemplateViewModel> ImageProcessingAlgorithmTemplateViewModels { get; set; }

        public ImageProcessingCustomizationViewModel()
        {
            ImageProcessingAlgorithmTemplateViewModels = new ObservableCollection<IImageProcessingAlgorithmTemplateViewModel>
            {
                new TwoPointNonUniformityTemplateViewModel(),
                new TwoPointNonUniformityTemplateViewModel(),
                new TwoPointNonUniformityTemplateViewModel(),
                new TwoPointNonUniformityTemplateViewModel(),
            };
        }
    }
}
