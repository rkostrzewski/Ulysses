using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public interface IProcessingChainBuilderDataStore
    {
        ProcessingChainTemplate ProcessingChainTemplate { get; set; }

        ProcessingStrategy ProcessingStrategy { get; set; }
    }
}