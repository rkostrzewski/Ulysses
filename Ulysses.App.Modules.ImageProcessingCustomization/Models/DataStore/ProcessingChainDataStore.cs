using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public class ProcessingChainDataStore : IProcessingChainBuilderDataStore
    {
        public ProcessingChainDataStore()
        {
            ProcessingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };
        }

        public ProcessingChainTemplate ProcessingChainTemplate { get; set; }
        public ProcessingStrategy ProcessingStrategy { get; set; }
    }
}