using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.ProcessingEngine.Factories
{
    public interface IProcessingEngineFactory
    {
        IProcessingEngine CreateInstance(ProcessingEngineTemplate template);
    }
}