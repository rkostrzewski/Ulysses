using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Modules.Navigation.Views;
using Ulysses.App.Regions;

namespace Ulysses.App.Modules.Navigation
{
    public class NavigationPanelModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public NavigationPanelModule(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegion.NavigationPanelRegion.ToString(), typeof (NavigationPanelView));
        }
    }
}