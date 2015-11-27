using System.Net;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Camera;

namespace Ulysses.ImageAcquisition.Factories
{
    public static class CameraAcquisitionStrategyFactory
    {
        public static CameraAcquisitionStrategy CreateInstance(IPEndPoint ipEndPoint, int timeout, ImageModel imageModel)
        {
            var udpClient = new UdpClient(ipEndPoint, timeout);
            
            return new CameraAcquisitionStrategy(imageModel, udpClient);
        }
    }
}
