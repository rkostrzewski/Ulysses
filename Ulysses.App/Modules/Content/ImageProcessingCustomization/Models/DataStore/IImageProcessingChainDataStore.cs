namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore
{
    public interface IImageProcessingChainDataStore
    {
        ImageProcessingChainElementsObservableCollection ImageProcessingChainElements { get; set; }
    }
}