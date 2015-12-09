using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Modules.ControlPanel.Views;
using Ulysses.App.Regions;

namespace Ulysses.App.Modules.ControlPanel
{
    public class ControlPanelModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public ControlPanelModule(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegions.ControlPanelRegion.ToString(), typeof (ControlPanelView));
        }
    }
}