using System.Collections.Generic;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Factories
{
    public interface IImageProcessingChainFactory
    {
        IImageProcessingChain BuildChain(IEnumerable<IImageProcessingAlgorithmTemplate> templates, ImageModel initialImageModel);
    }
}