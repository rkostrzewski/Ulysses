using System;
using Ulysses.Core.Templates;

namespace Ulysses.ProcessingAlgorithms.Templates
{
    public interface IImageProcessingAlgorithmTemplate : IProcessingChainElementTemplate
    {
        Type AlgorithmType { get; }

        ImageProcessingAlgorithmGroup Group { get; }
    }
}