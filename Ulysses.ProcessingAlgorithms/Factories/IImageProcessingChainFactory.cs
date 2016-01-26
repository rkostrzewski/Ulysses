using System.Collections.Generic;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public interface IImageProcessingChainFactory
    {
        IImageProcessingChain BuildChain(IEnumerable<IImageProcessingAlgorithmTemplate> templates);
    }
}