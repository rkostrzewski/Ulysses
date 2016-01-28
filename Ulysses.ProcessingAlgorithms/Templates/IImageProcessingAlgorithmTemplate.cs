using System;
using Ulysses.Core.Models;
using Ulysses.Core.Templates;

namespace Ulysses.ProcessingAlgorithms.Templates
{
    public interface IImageProcessingAlgorithmTemplate : IProcessingChainElementTemplate
    {
        ImageModel ImageModel { get; set; }

        Type AlgorithmType { get; }

        ImageProcessingAlgorithmGroup Group { get; }
    }
}