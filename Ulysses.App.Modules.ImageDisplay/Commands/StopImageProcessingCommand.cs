using System;
using Ulysses.App.Core.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;

namespace Ulysses.App.Modules.ImageDisplay.Commands
{
    public class StopImageProcessingCommand : NoParameterCommand, IStopImageProcessingCommand
    {
        private readonly IProcessingService _processingService;

        public StopImageProcessingCommand(IProcessingService processingService)
        {
            _processingService = processingService;
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new InvalidOperationException();
            }

            _processingService.ProcessingEngine?.Stop().ContinueWith((task) =>
            {
                OnProcessingStop?.Invoke();
            });
        }

        public override bool CanExecute()
        {
            return _processingService?.ProcessingEngine != null && _processingService.ProcessingEngine.IsWorking();
        }

        public Action OnProcessingStop { get; set; }
    }
}