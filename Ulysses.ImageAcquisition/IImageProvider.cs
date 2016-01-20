using Ulysses.Core.Models;

namespace Ulysses.ImageProviders
{
    public interface IImageProvider
    {
        bool TryToObtainImage(out Image image);
    }
}