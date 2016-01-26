using System.Collections.Generic;
using Ulysses.App.Core.Providers;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider
{
    public interface IImageProviderCustomizationViewModel
    {
        IList<Displayable<ImageBitDepth>> AvailableBitDepths { get; }
    }
}