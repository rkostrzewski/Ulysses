using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms
{
    public interface IImageProcessingChain
    {
        Image ProcessImage(Image image);
    }
}