using System;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class BrightnessAdjustmentTemplate : BaseAdjustmentTemplate
    {
        public override Type AlgorithmType => typeof (BrightnessAdjustmentAlgorithm);
    }
}