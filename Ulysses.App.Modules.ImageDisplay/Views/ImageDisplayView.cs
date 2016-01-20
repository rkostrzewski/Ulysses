using Ulysses.App.Modules.ImageDisplay.ViewModels;

namespace Ulysses.App.Modules.ImageDisplay.Views
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