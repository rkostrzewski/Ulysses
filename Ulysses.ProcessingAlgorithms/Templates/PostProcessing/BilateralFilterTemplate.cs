using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class BilateralFilterTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public override Type AlgorithmType => typeof (BilateralFilterAlgorithm);
        public override ImageProcessingAlgorithmGroup Group { get; }
        public ushort RangeKernel { get; set; }
        public ushort SpatialKernel { get; set; }
    }
}
