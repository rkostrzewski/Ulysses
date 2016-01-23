namespace Ulysses.App.Modules.ImageProcessingCustomization.Models
{
    public abstract class BaseImageProcessingChainElement : IImageProcessingChainElement
    {
        public string Id { get; set; }
        public abstract string ElementName { get; }
    }
}