namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Models
{
    public interface IImageProcessingChainElement
    {
        string Id { get; set; }

        string ElementName { get; }
    }
}