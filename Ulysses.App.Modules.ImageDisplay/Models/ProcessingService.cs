using Ulysses.ProcessingEngine.Factories;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.App.Modules.ImageDisplay.Models
{
    public class ProcessingService : IProcessingService
    {
        private readonly IProcessingEngineFactory _processingEngineFactory;

        public ProcessingService(IProcessingEngineFactory processingEngineFactory)
        {
            _processingEngineFactory = processingEngineFactory;
        }

        public IProcessingEngine ProcessingEngine { get; private set; }

        public void UpdateProcessingEngine(ProcessingEngineTemplate template)
        {
            ProcessingEngine = _processingEngineFactory.CreateInstance(template);
        }
    }
}