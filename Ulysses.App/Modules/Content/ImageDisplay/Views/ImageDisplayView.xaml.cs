using Ulysses.App.Modules.Content.ImageDisplay.ViewModels;

namespace Ulysses.App.Modules.Content.ImageDisplay.Views
{
    /// <summary>
    ///     Interaction logic for ImageDisplayView.xaml
    /// </summary>
    public partial class ImageDisplayView
    {
        public ImageDisplayView(IImageDisplayViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}