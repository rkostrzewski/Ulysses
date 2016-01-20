using System.Net;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.Camera;
using Ulysses.ImageProviders.Camera.Udp;

namespace Ulysses.ImageProviders.Factories
{
    public static class CameraImageProviderFactory
    {
        public static CameraImageProvider CreateInstance(IPEndPoint ipEndPoint, int timeout, ImageModel imageModel)
        {
            var udpClient = new UdpClient(ipEndPoint, timeout);

            return new CameraImageProvider(imageModel, udpClient);
        }
    }
}