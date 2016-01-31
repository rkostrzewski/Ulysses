using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates
{
    public class BilateralFilterTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public ushort SpatialKernel { get; set; }
        public ushort RangeKernel { get; set; }
        public override Type AlgorithmType => typeof (BilateralFilterAlgorithm);
        public override ImageProcessingAlgorithmGroup Group { get; }
    }
}