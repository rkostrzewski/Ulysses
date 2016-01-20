using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using Ulysses.App.Modules.ImageDisplay;
using Ulysses.App.Modules.ImageProcessingCustomization;
using Ulysses.App.Modules.Navigation;

namespace Ulysses.App
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            ContainerBootstrapper.ConfigureDependencies(Container);
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;

            moduleCatalog.AddModule(typeof (ImageDisplayModule));
            moduleCatalog.AddModule(typeof (ImageProcessingCustomizationModule));
            moduleCatalog.AddModule(typeof (NavigationModule), nameof(ImageDisplayModule), nameof(ImageProcessingCustomizationModule));
        }
    }
}