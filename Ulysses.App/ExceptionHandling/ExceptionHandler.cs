using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ulysses.App.ExceptionHandling.Handlers;

namespace Ulysses.App.ExceptionHandling
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static readonly IEnumerable<IInternalExceptionHandling> ExceptionHandlers = new List<IInternalExceptionHandling>
        {
            new ProcessingEngineExceptionHandler(),
            new ImageProviderExceptionHandler(),
            new ImageOperationsExceptionHandler(),
            new ProcessingAlgorithmExceptionHandling(),
            new ModuleExceptionHandling()
        };

        private readonly Window _mainWindow;

        public ExceptionHandler(Window mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public async Task HandleException(Exception exception)
        {
            const string errorDialogTittle = "An error has occured";
            var message = string.Empty;
            var isHandled = false;

            foreach (var result in ExceptionHandlers.Select(handler => handler.TryHandleException(exception)))
            {
                isHandled = result.IsResolved;
                message = result.Message;
                if (isHandled)
                {
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                message = "An unexpected error has occured. Application will abort now.";
            }

            await ShowExceptionMessage(errorDialogTittle, message);


            if (!isHandled)
            {
                throw exception;
            }
        }

        private async Task ShowExceptionMessage(string title, string message)
        {
            var metroWindow = _mainWindow as MetroWindow;

            if (metroWindow == null)
            {
                throw new Exception(message);
            }

            await metroWindow.ShowMessageAsync(title, message);
        }
    }
}