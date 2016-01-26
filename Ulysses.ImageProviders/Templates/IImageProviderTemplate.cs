using Ulysses.Core.Models;

namespace Ulysses.ImageProviders.Templates
{
    public interface IImageProviderTemplate
    {
        ImageProviderType ImageProviderType { get; set; }

        ImageModel ImageModel { get; set; }

        CameraImageProviderTemplate CameraImageProviderTemplate { get; }

        FileSystemImageProviderTemplate FileSystemImageProviderTemplate { get; }
    }
}