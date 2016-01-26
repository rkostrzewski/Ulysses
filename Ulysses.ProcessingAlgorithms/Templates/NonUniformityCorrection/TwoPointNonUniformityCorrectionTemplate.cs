using System;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionTemplate : BaseNonUniformityCorrectionTemplate
    {
        public override Type AlgorithmType => typeof (TwoPointNonUniformityCorrectionAlgorithm);

        public override string NonUniformityModelFilePath { get; set; }

        public override bool UseNonUniformityModel
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }
    }
}