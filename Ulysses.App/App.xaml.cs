using System;
using System.Windows;

namespace Ulysses.App
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            Current.DispatcherUnhandledException += (sender, eventArgs) =>
            {
                MessageBox.Show(eventArgs.Exception.Message, "Exception Caught", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception(eventArgs.Exception.Message, eventArgs.Exception);
            };
        }
    }
}