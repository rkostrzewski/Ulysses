using System;
using Ulysses.Core.Templates;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public abstract class BaseNonUniformityCorrectionTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public abstract string NonUniformityModelFilePath { get; set; }

        public abstract bool UseNonUniformityModel { get; set; }

        public NonUniformityModel NonUniformityModel { get; set; }
        public abstract Type AlgorithmType { get; }

        public ImageProcessingAlgorithmGroup Group => ImageProcessingAlgorithmGroup.NonUniformityCorrection;

        public override string ElementName => AlgorithmType.Name;
    }
}