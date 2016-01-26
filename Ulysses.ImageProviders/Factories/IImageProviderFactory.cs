using Ulysses.ImageProviders.Templates;

namespace Ulysses.ImageProviders.Factories
{
    public interface IImageProviderFactory
    {
        IImageProvider CreateInstance(IImageProviderTemplate template);
    }
}