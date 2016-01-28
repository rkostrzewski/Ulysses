using System;
using Ulysses.Core.Models;
using Ulysses.Core.Templates;

namespace Ulysses.ProcessingAlgorithms.Templates
{
    public abstract class BaseImageProcessingAlgorithmTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public ImageModel ImageModel { get; set; }

        public abstract Type AlgorithmType { get; }

        public abstract ImageProcessingAlgorithmGroup Group { get; }
    }
}