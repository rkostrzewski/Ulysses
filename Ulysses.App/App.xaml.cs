using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using Prism.Events;
using Ulysses.App.ExceptionHandling;

namespace Ulysses.App
{
    public partial class App
    {
        private IExceptionHandler _exceptionHandler;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            _exceptionHandler = new ExceptionHandler(Current.MainWindow);

            Current.DispatcherUnhandledException += (sender, eventArgs) => HandleException(eventArgs.Exception, eventArgs);
        }

        private async void HandleException(Exception exception, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            eventArgs.Handled = true;
            await _exceptionHandler.HandleException(exception);
        }
    }
}