using System.Threading;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms;

namespace Ulysses.ProcessingUtilities.Sleep
{
    public class Sleeper : IImageProcessingChain
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