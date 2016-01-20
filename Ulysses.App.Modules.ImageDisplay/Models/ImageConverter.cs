using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageDisplay.Models
{
    public class ImageConverter
    {
        private const int Dpi = 96;
        private static readonly PixelFormat PixelFormat = PixelFormats.Gray8;
        private static readonly BitmapPalette Palette = BitmapPalettes.Gray256;

        public static BitmapSource ConvertToBitmapSource(Image image)
        {
            if (image == null)
            {
                return null;
            }

            var width = image.ImageModel.Width;
            var height = image.ImageModel.Height;
            var pixels = image.ImagePixels.Select(p => (byte)p).ToArray();

            var rectangle = new Int32Rect(0, 0, width, height);
            var stride = width;

            var bitmap = new WriteableBitmap(width, height, Dpi, Dpi, PixelFormat, Palette);

            bitmap.Lock();
            bitmap.WritePixels(rectangle, pixels, stride, 0);
            bitmap.Unlock();

            return bitmap;
        }
    }
}