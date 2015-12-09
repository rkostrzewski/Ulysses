using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using Ulysses.App.Modules.Content.ImageDisplay;
using Ulysses.App.Modules.Content.ImageProcessingCustomization;
using Ulysses.App.Modules.ControlPanel;
using Ulysses.App.Views;

namespace Ulysses.App
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.ConfigureDependencies();
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
            moduleCatalog.AddModule(typeof (ControlPanelModule), nameof(ImageDisplayModule), nameof(ImageProcessingCustomizationModule));
        }
    }
}