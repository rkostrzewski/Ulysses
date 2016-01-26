using System;
using System.Threading.Tasks;
using Ulysses.App.Core.Commands;
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

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new InvalidOperationException();
            }

            _processingService.ProcessingEngine?.Start().ContinueWith((task) =>
            {
                OnProcessingStop?.Invoke();
            });
        }

        public override bool CanExecute()
        {
            return _processingService?.ProcessingEngine != null && !_processingService.ProcessingEngine.IsWorking();
        }

        public Action OnProcessingStop { get; set; }
    }
}