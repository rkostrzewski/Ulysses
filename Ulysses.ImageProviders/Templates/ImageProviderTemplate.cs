using Ulysses.Core.Models;
using Ulysses.Core.Templates;

namespace Ulysses.ImageProviders.Templates
{
    public class ImageProviderTemplate : BaseProcessingChainElementTemplate, IImageProviderTemplate
    {
        private static readonly ImageModel DefaultImageModel = new ImageModel(0, 0, ImageBitDepth.Bpp8);

        public ImageProviderTemplate()
        {
            CameraImageProviderTemplate = new CameraImageProviderTemplate();
            FileSystemImageProviderTemplate = new FileSystemImageProviderTemplate();
            ImageModel = DefaultImageModel;
        }

        public override string ElementName => "Input";
        public ImageProviderType ImageProviderType { get; set; }

        public CameraImageProviderTemplate CameraImageProviderTemplate { get; set; }

        public FileSystemImageProviderTemplate FileSystemImageProviderTemplate { get; set; }

        public ImageModel ImageModel { get; set; }
    }
}