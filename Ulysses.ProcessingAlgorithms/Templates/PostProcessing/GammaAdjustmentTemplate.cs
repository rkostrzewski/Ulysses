using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class GammaAdjustmentTemplate : BaseAdjustmentTemplate
    {
        public override Type AlgorithmType => typeof(GammaAdjustmentAlgorithm);
    }
}