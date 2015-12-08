using Ulysses.Core.Models;

namespace Ulysses.Core
{
    public interface IImageProcessingChain
    {
        Image ProcessImage(Image image);
    }
}