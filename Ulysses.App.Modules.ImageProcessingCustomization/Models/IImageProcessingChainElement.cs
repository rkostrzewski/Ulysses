namespace Ulysses.App.Modules.ImageProcessingCustomization.Models
{
    public interface IImageProcessingChainElement
    {
        string Id { get; set; }

        string ElementName { get; }
    }
}