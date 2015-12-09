using Microsoft.Practices.Unity;
using Ulysses.App.Modules.ControlPanel.ViewModels;

namespace Ulysses.App
{
    public static class ContainerConfiguration
    {
        public static void ConfigureDependencies(this IUnityContainer container)
        {
            RegisterViewModels(container);
        }

        private static void RegisterViewModels(IUnityContainer container)
        {
            container.RegisterType<IControlPanelViewModel, ControlPanelViewModel>();
        }
    }
}
