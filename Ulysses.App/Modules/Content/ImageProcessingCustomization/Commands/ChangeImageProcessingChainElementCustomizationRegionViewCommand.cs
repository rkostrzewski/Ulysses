using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Regions.ViewLocators;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Commands
{
    public class ChangeImageProcessingChainElementCustomizationRegionViewCommand : NavigateToImageProcessingChainElementCustomizationViewCommand,
                                                                                   IChangeImageProcessingChainElementCustomizationRegionViewCommand
    {
        public ChangeImageProcessingChainElementCustomizationRegionViewCommand(IRegionManager regionManager, IImageProcessingChainElementViewLocator viewLocator)
            : base(regionManager, ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), viewLocator)
        {
        }
    }
}