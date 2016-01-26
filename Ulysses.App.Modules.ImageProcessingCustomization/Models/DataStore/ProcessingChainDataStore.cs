using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public class ProcessingChainDataStore : IProcessingChainBuilderDataStore
    {
        public ProcessingChainDataStore()
        {
            ProcessingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };
        }

        public ProcessingChainTemplate ProcessingChainTemplate { get; set; }
    }
}