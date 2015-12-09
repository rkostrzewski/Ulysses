using System;
using System.Threading.Tasks;
using Ulysses.App.Utils;
using Ulysses.ProcessingEngine;

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
            _processingEngine.Start();
        }

        public override bool CanExecute()
        {
            return !_processingEngine.IsWorking();
        }

        public override event EventHandler CanExecuteChanged;
    }
}