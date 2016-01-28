using System;
using System.IO;
using System.Net;
using Ulysses.Core.Models;
using Ulysses.ImageProviders.Camera;
using Ulysses.ImageProviders.Camera.Udp;
using Ulysses.ImageProviders.FileSystem;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.ImageProviders.Factories
{
    public class ImageProviderFactory : IImageProviderFactory
    {
        public IImageProvider CreateInstance(IImageProviderTemplate template)
        {
            switch (template.ImageProviderType)
            {
                case ImageProviderType.CameraProvider:
                    return CreateImageProviderInstance(template.ImageModel, template.CameraImageProviderTemplate);
                case ImageProviderType.FileSystemProvider:
                    return CreateFileSystemProviderInstance(template.ImageModel, template.FileSystemImageProviderTemplate);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static CameraImageProvider CreateImageProviderInstance(ImageModel imageModel, CameraImageProviderTemplate template)
        {
            var port = template.Port;
            var timeout = template.Timeout;

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(CameraImageProvider.LocalhostIpAddress), port);
            var udpClient = new UdpClient(ipEndPoint, timeout);

            return new CameraImageProvider(imageModel, udpClient);
        }

        private static IImageProvider CreateFileSystemProviderInstance(ImageModel imageModel, FileSystemImageProviderTemplate template)
        {
            var files = Directory.EnumerateFiles(template.FolderPath, template.FileSearchPattern);

            return new FileSystemImageProvider(imageModel, files, template.InfiniteLoop);
        }
    }
}