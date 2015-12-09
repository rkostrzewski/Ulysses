using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine.ImageProcessingChain
{
    public interface IImageProcessingChain
    {
        Image ProcessImage(Image image);
    }
}