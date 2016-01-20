using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public class ImageProcessingChainDataStore : IImageProcessingChainDataStore
    {
        public ImageProcessingChainDataStore()
        {
            ImageProcessingChainElements = new ImageProcessingChainElementsObservableCollection { new ImageProviderTemplate(), new ImageDisplayTemplate() };
        }

        public ImageProcessingChainElementsObservableCollection ImageProcessingChainElements { get; set; }
    }
}