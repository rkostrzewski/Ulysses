using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Image = Ulysses.Core.Models.Image;

namespace Ulysses.ImageProviders.FileSystem.ImageReaders
{
    public class BitmapImageReader : IImageReader
    {
        private readonly ImageModel _imageModel;

        public BitmapImageReader(ImageModel imageModel)
        {
            _imageModel = imageModel;
        }

        public Image Read(string filePath)
        {
            var inputImage = new Bitmap(System.Drawing.Image.FromFile(filePath));
            if (inputImage.Width != _imageModel.Width || inputImage.Height != _imageModel.Height)
            {
                throw new ImageModelMismatchException(typeof (BitmapImageReader));
            }

            return ConvertToImage(inputImage);
        }

        private Image ConvertToImage(Bitmap image)
        {
            var rgbaPixels = GetImagePixels(image);
            var grayscalePixels = ConvertRgbaPixelsToGrayscale(rgbaPixels);

            return new Image(grayscalePixels, _imageModel);
        }

        private static byte[] GetImagePixels(Bitmap image)
        {
            var pixelArea = new Rectangle(0, 0, image.Width, image.Height);
            var bitmapData = image.LockBits(pixelArea, ImageLockMode.ReadWrite, image.PixelFormat);
            var pointerToPixels = bitmapData.Scan0;

            var numberOfPixels = bitmapData.Stride * image.Height;
            var pixels = new byte[numberOfPixels];

            Marshal.Copy(pointerToPixels, pixels, 0, numberOfPixels);

            image.UnlockBits(bitmapData);
            return pixels;
        }

        private static IEnumerable<byte> ConvertRgbaPixelsToGrayscale(IReadOnlyList<byte> rgbaPixels)
        {
            var pixelCount = rgbaPixels.Count / 4;

            return
                Enumerable.Range(0, pixelCount)
                          .Select(i => ConvertRgbaPixelToGrayscale(rgbaPixels[i * 4], rgbaPixels[i * 4 + 1], rgbaPixels[i * 4 + 2], rgbaPixels[i * 4 + 3]));
        }

        private static byte ConvertRgbaPixelToGrayscale(byte red, byte green, byte blue, byte alpha)
        {
            const float redFactor = 0.3f;
            const float greenFactor = 0.59f;
            const float blueFactor = 0.11f;

            return (byte)((red * redFactor + green * greenFactor + blue * blueFactor) * (alpha / (float)byte.MaxValue));
        }
    }
}