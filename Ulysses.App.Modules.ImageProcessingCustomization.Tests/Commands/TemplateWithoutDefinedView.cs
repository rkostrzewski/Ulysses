using Ulysses.App.Modules.ImageProcessingCustomization.Models;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Commands
{
    public class TemplateWithoutDefinedView : IImageProcessingChainElement
    {
        public string Id { get; set; }
        public string ElementName => nameof(TemplateWithoutDefinedView);
    }
}