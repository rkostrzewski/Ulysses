using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views;
using Ulysses.App.Regions;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization
{
    public class ImageProcessingCustomizationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ImageProcessingCustomizationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ApplicationRegions.ContentRegion, typeof (ImageProcessingCustomizationView));
        }
    }
}