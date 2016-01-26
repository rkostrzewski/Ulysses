using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay
{
    public class ImageDisplayTemplateTemplate : BaseProcessingChainElementTemplate, IImageDisplayTemplate
    {
        public override string ElementName => "Output";
    }
}