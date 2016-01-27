using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.Exception;

namespace Ulysses.ImageProviders.Camera.CameraStream
{
    public sealed class CameraStreamConfiguration
    {
        public CameraStreamConfiguration(ImageModel imageModel)
        {
            if (imageModel.Width != ImageWidth || imageModel.Height != ImageHeight)
            {
                throw new NotSupportedImageSizeForCameraProviderException();
            }

            switch (imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    SizeOfPixelsInPacket = 1024;
                    PacketAmount = 768;
                    break;
                case ImageBitDepth.Bpp12:
                    SizeOfPixelsInPacket = 768;
                    PacketAmount = 1536;
                    break;
            }
        }

        public int ImageWidth => 1024;
        public int ImageHeight => 768;
        public int Padding => 2;

        public int SizeOfPixelsInPacket { get; }
        public int PacketAmount { get; }
    }
}