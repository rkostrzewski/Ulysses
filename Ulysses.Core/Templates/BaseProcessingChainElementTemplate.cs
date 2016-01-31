namespace Ulysses.Core.Templates
{
    public abstract class BaseProcessingChainElementTemplate : IProcessingChainElementTemplate
    {
        public string Id { get; set; }

        public abstract string ElementName { get; }
    }
}