using Prism.Events;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.App.Core.Events
{
    public class ShouldUpdateProcessingEngineEvent : PubSubEvent<ProcessingEngineTemplate>
    {
    }
}