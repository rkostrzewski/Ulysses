using System;
using Ulysses.Core.Models;
using Ulysses.Core.Templates;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public abstract class BaseNonUniformityCorrectionTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public ImageModel ImageModel { get; set; }

        public abstract Type AlgorithmType { get; }

        public ImageProcessingAlgorithmGroup Group => ImageProcessingAlgorithmGroup.NonUniformityCorrection;

        public override string ElementName => AlgorithmType.Name;
    }
}