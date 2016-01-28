using System;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public class ConstantRangeNonUniformityCorrectionTemplate : BaseNonUniformityCorrectionTemplate
    {
        public override Type AlgorithmType => typeof (ConstantRangeNonUniformityCorrectionAlgorithm);
        public ushort RangeMinimum { get; set; }
        public ushort RangeMaximum { get; set; }
        public ushort AmountOfProcessedImagesToStopCalibration { get; set; }
    }
}
