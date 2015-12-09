using Ulysses.Core.Models;

namespace Ulysses.ImageAcquisition
{
    public interface IImageAcquisition
    {
        bool TryToObtainImage(out Image image);
    }
}