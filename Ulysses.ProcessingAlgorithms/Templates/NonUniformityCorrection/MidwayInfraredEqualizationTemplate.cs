using System;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public class MidwayInfraredEqualizationTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public double StandardDeviation { get; set; }
        public override Type AlgorithmType => typeof (MidwayInfraredEqualization);
        public override ImageProcessingAlgorithmGroup Group { get; }
    }
}