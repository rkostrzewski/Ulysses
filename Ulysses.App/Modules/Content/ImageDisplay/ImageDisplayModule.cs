using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Modules.Content.ImageDisplay.Views;
using Ulysses.App.Regions;

namespace Ulysses.App.Modules.Content.ImageDisplay
{
    public class ImageDisplayModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public ImageDisplayModule(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegions.ContentRegion.ToString(), typeof (ImageDisplayView));
        }
    }
}