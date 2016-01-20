using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.ImageDisplay;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore
{
    public class ImageProcessingChainDataStore : IImageProcessingChainDataStore
    {
        public ImageProcessingChainDataStore()
        {
            ImageProcessingChainElements = new ImageProcessingChainElementsObservableCollection { new ImageAcquisitionTemplate(), new ImageDisplayTemplate() };
        }

        public ImageProcessingChainElementsObservableCollection ImageProcessingChainElements { get; set; }
    }
}