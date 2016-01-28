using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class ContrastAdjustmentTemplate : BaseAdjustmentTemplate
    {
        public override Type AlgorithmType => typeof (ContrastAdjustmentAlgorithm);
    }
}