using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageDisplay.ViewModels;
using Ulysses.App.Modules.ImageDisplay.Views;

namespace Ulysses.App.Modules.ImageDisplay
{
    public class ImageDisplayModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public ImageDisplayModule(IRegionViewRegistry regionViewRegistry, IUnityContainer container)
        {
            _regionViewRegistry = regionViewRegistry;
            RegisterModuleDependencies(container);
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegion.ContentRegion.ToString(), typeof (ImageDisplayView));
        }

        private static void RegisterModuleDependencies(IUnityContainer container)
        {
            container.RegisterType<IImageDisplayViewModel, ImageDisplayViewModel>();
        }
    }
}