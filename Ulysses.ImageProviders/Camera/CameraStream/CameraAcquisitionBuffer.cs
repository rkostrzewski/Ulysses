using System;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.ImageProviders.Camera.CameraStream
{
    internal class CameraAcquisitionBuffer
    {
        private readonly CameraStreamConfiguration _cameraStreamConfiguration;
        private readonly CameraStreamToImageConverter _cameraStreamToImageConverter;
        private readonly byte[][] _receivedPixelPackets;

        public CameraAcquisitionBuffer(ImageModel imageModel)
        {
            _cameraStreamConfiguration = new CameraStreamConfiguration(imageModel);
            _cameraStreamToImageConverter = new CameraStreamToImageConverter(imageModel);

            var sizeOfPacket = _cameraStreamConfiguration.SizeOfPixelsInPacket;
            var packetAmount = _cameraStreamConfiguration.PacketAmount;

            _receivedPixelPackets = new byte[packetAmount][];
            for (var i = 0; i < packetAmount; i++)
            {
                _receivedPixelPackets[i] = new byte[sizeOfPacket];
            }
        }

        public void PlacePacketInBuffer(byte[] packet)
        {
            if (packet.Length != _cameraStreamConfiguration.SizeOfPixelsInPacket + _cameraStreamConfiguration.Padding)
            {
                throw new ImageModelMismatchException();
            }

            var packetId = packet[0] * 100 + packet[1];

            Array.Copy(packet, 2, _receivedPixelPackets[packetId], 0, _cameraStreamConfiguration.SizeOfPixelsInPacket);
        }

        public Image GetImage()
        {
            return _cameraStreamToImageConverter.GetImage(_receivedPixelPackets);
        }
    }
}