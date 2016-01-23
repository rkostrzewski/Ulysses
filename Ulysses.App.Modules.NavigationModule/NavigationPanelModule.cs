using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.Models;
using Ulysses.App.Modules.Navigation.ViewModels;
using Ulysses.App.Modules.Navigation.Views;

namespace Ulysses.App.Modules.Navigation
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
            container.RegisterType<IChangeContentRegionCompositeCommand, ChangeContentRegionCompositeCommand>();
            container.RegisterType<INavigationPanelViewModel, NavigationPanelViewModel>();
        }
    }
}