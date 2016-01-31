using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class DestripeTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public override Type AlgorithmType => typeof (DestripeAlgorithm);

        public override ImageProcessingAlgorithmGroup Group { get; }

        public ushort Radius { get; set; }
    }
}