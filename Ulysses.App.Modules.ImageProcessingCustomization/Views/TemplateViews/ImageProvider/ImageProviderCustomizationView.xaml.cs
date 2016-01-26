using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.ImageProvider
{
    /// <summary>
    /// Interaction logic for ImageProviderCustomizationView.xaml
    /// </summary>
    public partial class ImageProviderCustomizationView
    {
        public ImageProviderCustomizationView(IImageProviderCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
