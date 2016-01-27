using System;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.App.Modules.ImageDisplay.Commands
{
    public interface ISetOutputImageCommand : IReceiveProcessedImageCommand
    {
        [CanBeNull]
        Action<BitmapSource> OnImageUpdate { get; set; }
    }
}