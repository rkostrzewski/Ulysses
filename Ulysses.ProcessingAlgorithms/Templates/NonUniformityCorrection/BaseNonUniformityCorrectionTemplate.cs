using System;
using Ulysses.Core.Templates;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public abstract class BaseNonUniformityCorrectionTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public abstract Type AlgorithmType { get; }

        public ImageProcessingAlgorithmGroup Group => ImageProcessingAlgorithmGroup.NonUniformityCorrection;

        public abstract string NonUniformityModelFilePath { get; set; }

        public abstract bool UseNonUniformityModel { get; set; }

        public override string ElementName => AlgorithmType.Name;

        public NonUniformityModel NonUniformityModel { get; set; }
    }
}