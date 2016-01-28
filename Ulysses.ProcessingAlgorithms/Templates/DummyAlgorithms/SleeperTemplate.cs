using System;
using Ulysses.ProcessingAlgorithms.Algorithms.DummyAlgorithms;

namespace Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms
{
    public class SleeperTemplate : BaseImageProcessingAlgorithmTemplate, IImageProcessingAlgorithmTemplate
    {
        public int SleepTimeInMilliseconds { get; set; }

        public override string ElementName => nameof(Sleeper);

        public override ImageProcessingAlgorithmGroup Group { get; }

        public override Type AlgorithmType => typeof (Sleeper);
    }
}