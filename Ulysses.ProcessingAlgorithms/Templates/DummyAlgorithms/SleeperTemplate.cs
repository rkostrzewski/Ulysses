using System;
using Ulysses.Core.Templates;
using Ulysses.ProcessingAlgorithms.Algorithms.DummyAlgorithms;

namespace Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms
{
    public class SleeperTemplate : BaseProcessingChainElementTemplate, IImageProcessingAlgorithmTemplate
    {
        public int SleepTimeInMilliseconds { get; set; }

        public override string ElementName => nameof(Sleeper);

        public Type AlgorithmType => typeof (Sleeper);

        public ImageProcessingAlgorithmGroup Group { get; }
    }
}