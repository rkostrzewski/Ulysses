using System.Windows.Media.Imaging;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageDisplay.Models
{
    public interface IImageConverter
    {
        BitmapSource ConvertToBitmapSource(Image image);
    }
}