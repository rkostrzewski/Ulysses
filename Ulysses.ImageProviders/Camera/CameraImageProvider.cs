using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.Camera.CameraStream;
using Ulysses.ImageProviders.Camera.Udp;

namespace Ulysses.ImageProviders.Camera
{
    public class CameraImageProvider : IImageProvider
    {
        public const string LocalhostIpAddress = "127.0.0.1";
        private readonly ImageModel _imageModel;
        private readonly IUdpClient _udpClient;

        public CameraImageProvider(ImageModel imageModel, IUdpClient udpClient)
        {
            _imageModel = imageModel;
            _udpClient = udpClient;
        }

        public bool TryToObtainImage(out Image image)
        {
            while (true)
            {
                var packet = _udpClient.Receive().ToArray();

                if (!IsStartPacket(packet))
                {
                    continue;
                }

                image = ObtainImage();
                return true;
            }
        }

        ~CameraImageProvider()
        {
            _udpClient.Close();
        }

        public void Dispose()
        {
            _udpClient.Close();
        }

        private Image ObtainImage()
        {
            var buffer = new CameraAcquisitionBuffer(_imageModel);

            while (true)
            {
                var packet = _udpClient.Receive().ToArray();

                if (IsStartPacket(packet))
                {
                    break;
                }

                buffer.PlacePacketInBuffer(packet);
            }

            return buffer.GetImage();
        }

        private static bool IsStartPacket(IReadOnlyCollection<byte> packet)
        {
            const string startWord = "START";

            var startPacket = Encoding.ASCII.GetBytes(startWord);

            return packet.Count >= startPacket.Length && startPacket.SequenceEqual(packet.Take(startPacket.Length));
        }
    }
}