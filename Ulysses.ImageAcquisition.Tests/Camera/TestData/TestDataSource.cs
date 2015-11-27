using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Camera;

namespace Ulysses.ImageAcquisition.Tests.Camera.TestData
{
    public static class TestDataSource
    {
        private const int PixelValue = 15;

        public static Image GetExpectedDummyImage(ImageModel imageModel)
        {
            var imagePixels = Enumerable.Range(0, imageModel.Height * imageModel.Width).Select(pixel => (Pixel)PixelValue);
            var image = new Image(imagePixels, imageModel);

            return image;
        }

        public static IEnumerable<byte[]> SimulateSendingOfDummyImage(ImageModel imageModel)
        {
            var cameraConfiguration = new CameraStreamConfiguration(imageModel);

            yield return Encoding.ASCII.GetBytes("START");

            foreach (var bytes in SimulateSendingOfPackages(imageModel, cameraConfiguration))
            {
                yield return bytes;
            }


            yield return Encoding.ASCII.GetBytes("START");
        }

        public static IEnumerable<byte[]> SimulateSendingOfDummyImageWithStartMidwayOfSendingAnImage(ImageModel imageModel)
        {
            var cameraConfiguration = new CameraStreamConfiguration(imageModel);

            foreach (var bytes in SimulateSendingOfPackages(imageModel, cameraConfiguration))
            {
                yield return bytes;
            }

            yield return Encoding.ASCII.GetBytes("START");

            foreach (var bytes in SimulateSendingOfPackages(imageModel, cameraConfiguration))
            {
                yield return bytes;
            }

            yield return Encoding.ASCII.GetBytes("START");
        }

        private static IEnumerable<byte[]> SimulateSendingOfPackages(ImageModel imageModel, CameraStreamConfiguration cameraConfiguration)
        {
            switch (imageModel.ImageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    foreach (var packet in SimulateSendingOfDummyImage8Bpp(cameraConfiguration))
                    {
                        yield return packet;
                    }

                    break;
                case ImageBitDepth.Bpp12:
                    foreach (var packet in SimulateSendingOfDummyImage12Bpp(cameraConfiguration))
                    {
                        yield return packet;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IEnumerable<byte[]> SimulateSendingOfDummyImage8Bpp(CameraStreamConfiguration cameraConfiguration)
        {
            for (var x = 0; x < cameraConfiguration.PacketAmount; x++)
            {
                var packet = new List<byte> { (byte)(x / 100), (byte)(x % 100) };

                packet.AddRange(Enumerable.Range(0, cameraConfiguration.SizeOfPixelsInPacket).Select(i => (byte)PixelValue));

                yield return packet.ToArray();
            }
        }

        private static IEnumerable<byte[]> SimulateSendingOfDummyImage12Bpp(CameraStreamConfiguration cameraConfiguration)
        {
            for (var x = 0; x < cameraConfiguration.PacketAmount; x++)
            {
                var packet = new List<byte> { (byte)(x / 100), (byte)(x % 100) };

                packet.AddRange(Enumerable.Range(0, cameraConfiguration.SizeOfPixelsInPacket).Select(i =>
                {
                    switch (i % 3)
                    {
                        case 0:
                            return (byte)0;
                        case 1:
                            return (byte)240;
                        case 2:
                            return (byte)15;
                        default:
                            throw new InvalidOperationException();
                    }
                }));

                yield return packet.ToArray();
            }
        }
    }
}