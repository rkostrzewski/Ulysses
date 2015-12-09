using System.Net;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Remote;
using Ulysses.ImageAcquisition.Remote.Udp;

namespace Ulysses.ImageAcquisition.Factories
{
    public static class CameraAcquisitionFactory
    {
        public static RemoteAcquisition CreateInstance(IPEndPoint ipEndPoint, int timeout, ImageModel imageModel)
        {
            var udpClient = new UdpClient(ipEndPoint, timeout);

            return new RemoteAcquisition(imageModel, udpClient);
        }
    }
}