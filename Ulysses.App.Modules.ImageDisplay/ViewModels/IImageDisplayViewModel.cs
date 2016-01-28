using System.Windows.Media.Imaging;
using Ulysses.App.Modules.ImageDisplay.Commands;

namespace Ulysses.App.Modules.ImageDisplay.ViewModels
{
    public interface IImageDisplayViewModel
    {
        ISetOutputImageCommand SetOutputImageCommand { get; }
        BitmapSource OutputImage { get; set; }
        IStartImageProcessingCommand StartImageProcessingCommand { get; }
        IStopImageProcessingCommand StopImageProcessingCommand { get; }
    }
}