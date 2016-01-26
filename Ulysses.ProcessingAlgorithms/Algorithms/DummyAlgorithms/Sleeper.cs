using System.Threading;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;

namespace Ulysses.ProcessingAlgorithms.Algorithms.DummyAlgorithms
{
    public class Sleeper : IImageProcessingAlgorithm
    {
        private readonly SleeperTemplate _template;

        public Sleeper(SleeperTemplate template)
        {
            _template = template;
        }

        public Image ProcessImage(Image inputImage)
        {
            Thread.Sleep(_template.SleepTimeInMilliseconds);
            return inputImage;
        }
    }
}