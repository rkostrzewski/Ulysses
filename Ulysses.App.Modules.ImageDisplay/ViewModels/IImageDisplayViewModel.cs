using System.Windows.Media.Imaging;
using Ulysses.App.Modules.Content.ImageDisplay.Commands;

namespace Ulysses.App.Modules.Content.ImageDisplay.ViewModels
{
    public interface IImageDisplayViewModel
    {
        IStartImageProcessingCommand StartImageProcessingCommand { get; }

        IStopImageProcessingCommand StopImageProcessingCommand { get; }

        BitmapSource OutputImage { get; }
    }
}