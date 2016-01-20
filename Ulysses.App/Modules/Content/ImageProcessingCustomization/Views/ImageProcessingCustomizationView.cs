using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Views
{
    public partial class ImageProcessingCustomizationView
    {
        public ImageProcessingCustomizationView(IImageProcessingCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}