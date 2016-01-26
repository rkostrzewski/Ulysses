namespace Ulysses.Core.Templates
{
    public interface IProcessingChainElementTemplate
    {
        string Id { get; set; }

        string ElementName { get; }
    }
}