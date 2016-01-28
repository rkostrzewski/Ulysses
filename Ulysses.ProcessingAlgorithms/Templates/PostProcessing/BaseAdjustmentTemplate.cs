using System;
using Ulysses.Core.Models;
using Ulysses.Core.Templates;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public abstract class BaseAdjustmentTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public ImageModel ImageModel { get; set; }
        public abstract Type AlgorithmType { get; }
        public ImageProcessingAlgorithmGroup Group { get; }
        public double AdjustmentValue { get; set; }
        public override string ElementName => AlgorithmType.Name;
    }
}