using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Remote.Camera;
using Ulysses.ImageAcquisition.Remote.Udp;

namespace Ulysses.ImageAcquisition.Remote
{
    public class RemoteAcquisition : IImageAcquisition
    {
        private readonly ImageModel _imageModel;
        private readonly IUdpClient _udpClient;

        public RemoteAcquisition(ImageModel imageModel, IUdpClient udpClient)
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

        ~RemoteAcquisition()
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