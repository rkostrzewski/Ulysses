using System;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionTemplate : BaseNonUniformityCorrectionTemplate
    {
        public TwoPointNonUniformityCorrectionTemplate()
        {
            NonUniformityModelFilePath = string.Empty;
        }

        public override Type AlgorithmType => typeof (TwoPointNonUniformityCorrectionAlgorithm);

        public NonUniformityModel NonUniformityModel { get; set; }

        public string NonUniformityModelFilePath { get; set; }

    }
}