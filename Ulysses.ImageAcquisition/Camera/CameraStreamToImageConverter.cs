using System.Linq;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;

namespace Ulysses.ImageAcquisition.Camera
{
    internal sealed class CameraStreamToImageConverter
    {
        private readonly CameraStreamConfiguration _cameraStreamConfiguration;
        private readonly ImageModel _imageModel;

        public CameraStreamToImageConverter(ImageModel imageModel)
        {
            _imageModel = imageModel;
            _cameraStreamConfiguration = new CameraStreamConfiguration(imageModel);
        }

        public Image GetImage(byte[][] receivedPixelPackets)
        {
            var receivedPixels = receivedPixelPackets.SelectMany(i => i).ToArray();

            return _imageModel.ImageBitDepth == ImageBitDepth.Bpp12 ? Get12BppImage(receivedPixels) : Get8BppImage(receivedPixels);
        }

        private Image Get12BppImage(byte[] receivedPixels)
        {
            var imageWidth = _cameraStreamConfiguration.ImageWidth;
            var imageHeight = _cameraStreamConfiguration.ImageHeight;

            var outputPixels = new ushort[imageHeight * imageWidth];

            for (int x = 0, k = 0; x < imageHeight * imageWidth; x = x + 2, k = k + 3)
            {
                    outputPixels[x] = (ushort)(receivedPixels[k] * 16 + receivedPixels[k + 1] / 16);
                    outputPixels[x + 1] = (ushort)((15 & receivedPixels[k + 1]) * 256 + receivedPixels[k + 2]);
            }

            return new Image(outputPixels, _imageModel);
        }

        private Image Get8BppImage(byte[] receivedPixels)
        {
            return new Image(receivedPixels, _imageModel);
        }
    }
}