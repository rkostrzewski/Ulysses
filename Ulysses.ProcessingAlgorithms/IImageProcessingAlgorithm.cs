using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms
{
    public interface IImageProcessingAlgorithm
    {
        Image ProcessImage(Image inputImage);
    }
}