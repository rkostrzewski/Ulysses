using System;
using Ulysses.App.Utils.Commands;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.Content.ImageDisplay.Commands
{
    public class StartImageProcessingCommand : NoParameterCommand, IStartImageProcessingCommand
    {
        private readonly IProcessingEngine _processingEngine;

        public StartImageProcessingCommand(IProcessingEngine processingEngine)
        {
            _processingEngine = processingEngine;
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new InvalidOperationException();
            }

            _processingEngine.Start();
        }

        public override bool CanExecute()
        {
            return _processingEngine != null && !_processingEngine.IsWorking();
        }
    }
}