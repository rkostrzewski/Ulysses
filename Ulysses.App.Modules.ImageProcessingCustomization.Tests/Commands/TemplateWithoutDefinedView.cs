using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Commands
{
    public class TemplateWithoutDefinedView : IProcessingChainElementTemplate
    {
        public string Id { get; set; }
        public string ElementName => nameof(TemplateWithoutDefinedView);
    }
}