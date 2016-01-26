using JetBrains.Annotations;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.App.Modules.ImageDisplay.Models
{
    public interface IProcessingService
    {
        [CanBeNull]
        IProcessingEngine ProcessingEngine { get; }

        void UpdateProcessingEngine(ProcessingEngineTemplate template);
    }
}