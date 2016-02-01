using System;
using Ulysses.Core.Models;

namespace Ulysses.ImageProviders
{
    public interface IImageProvider : IDisposable
    {
        bool TryToObtainImage(out Image image);
    }
}