using System.Collections.Generic;
using Ulysses.App.Core.Providers;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.Core.Models;
using Ulysses.ImageProviders;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider
{
    public interface IImageProviderCustomizationViewModel
    {
        ISelectFolderCommand SelectFolderCommand { get; }
        IList<Displayable<ImageProviderType>> AvailableImageProviders { get; }
        int SelectedTabIndex { get; set; }
        ImageProviderType SelectedImageProvider { get; set; }
        ImageBitDepth ImageBitDepth { get; set; }
        ushort ImageWidth { get; set; }
        ushort ImageHeight { get; set; }
        int Port { get; set; }
        int Timeout { get; set; }
        string FolderPath { get; set; }
        string FileSearchPattern { get; set; }
        IList<Displayable<ImageBitDepth>> AvailableBitDepths { get; }
        bool InfiniteLoop { get; set; }
    }
}