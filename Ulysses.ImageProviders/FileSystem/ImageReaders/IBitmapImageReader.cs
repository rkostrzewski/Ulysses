using Ulysses.Core.Models;

namespace Ulysses.ImageProviders.FileSystem.ImageReaders
{
    public interface IImageReader
    {
        Image Read(string filePath);
    }
}