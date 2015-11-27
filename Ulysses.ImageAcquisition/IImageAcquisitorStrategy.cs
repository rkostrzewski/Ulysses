using Ulysses.Core.Models;

namespace Ulysses.ImageAcquisition
{
    public interface IImageAcquisitorStrategy
    {
        bool TryToObtainImage(out Image image);
    }
}