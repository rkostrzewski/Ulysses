using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.NavigationModule.Commands;
using Ulysses.App.Modules.NavigationModule.Models;
using Ulysses.App.Modules.NavigationModule.ViewModels;
using Ulysses.App.Modules.NavigationModule.Views;

namespace Ulysses.App.Modules.NavigationModule
{
    public class NavigationModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public NavigationModule(IRegionViewRegistry regionViewRegistry, IUnityContainer container)
        {
            _regionViewRegistry = regionViewRegistry;
            RegisterModuleDependencies(container);
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegion.NavigationPanelRegion.ToString(), typeof (NavigationPanelView));
        }

        private static void RegisterModuleDependencies(IUnityContainer container)
        {
            container.RegisterInstance(typeof(INavigationPanelState), new NavigationPanelState());
            container.RegisterType<IChangeContentRegionsViewCommand, ChangeContentRegionsViewCommand>();
            container.RegisterType<IChangeCurrentRegionInNavigationPanelCommand, ChangeCurrentRegionInNavigationPanelCommand>();
            container.RegisterType<INavigationPanelViewModel, NavigationPanelViewModel>();
        }
    }
}