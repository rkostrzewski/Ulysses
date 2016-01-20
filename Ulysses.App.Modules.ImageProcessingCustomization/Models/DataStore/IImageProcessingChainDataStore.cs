namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public interface IImageProcessingChainDataStore
    {
        ImageProcessingChainElementsObservableCollection ImageProcessingChainElements { get; set; }
    }
}