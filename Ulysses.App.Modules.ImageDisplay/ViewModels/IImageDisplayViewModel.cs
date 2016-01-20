using System.Windows.Media.Imaging;
using Ulysses.App.Modules.ImageDisplay.Commands;

namespace Ulysses.App.Modules.ImageDisplay.ViewModels
{
    public interface IImageDisplayViewModel
    {
        IStartImageProcessingCommand StartImageProcessingCommand { get; }

        IStopImageProcessingCommand StopImageProcessingCommand { get; }

        BitmapSource OutputImage { get; }
    }
}