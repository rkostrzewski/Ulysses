using System;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageDisplay.Models;

namespace Ulysses.App.Modules.ImageDisplay.Commands
{
    public class StartImageProcessingCommand : NoParameterCommand, IStartImageProcessingCommand
    {
        private readonly IProcessingService _processingService;

        public StartImageProcessingCommand(IProcessingService processingService)
        {
            _processingService = processingService;
        }

        public override async void Execute()
        {
            if (!CanExecute())
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var processingTask = _processingService.ProcessingEngine?.Start();

            if (processingTask == null)
            {
                return;
            }

            await processingTask;

            OnProcessingStop?.Invoke();

            if (!processingTask.IsFaulted)
            {
                return;
            }

            var baseException = processingTask.Exception?.GetBaseException();
            if (baseException != null)
            {
                throw baseException;
            }
        }

        public override bool CanExecute()
        {
            return _processingService?.ProcessingEngine != null && !_processingService.ProcessingEngine.IsWorking();
        }

        public Action OnProcessingStop { get; set; }
    }
}