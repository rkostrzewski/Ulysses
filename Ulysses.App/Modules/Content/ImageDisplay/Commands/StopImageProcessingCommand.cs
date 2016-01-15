﻿using System;
using Ulysses.App.Utils;
using Ulysses.App.Utils.Commands;
using Ulysses.ProcessingEngine.ProcessingEngine;

namespace Ulysses.App.Modules.Content.ImageDisplay.Commands
{
    public class StopImageProcessingCommand : NoParameterCommand, IStopImageProcessingCommand
    {
        private readonly IProcessingEngine _processingEngine;

        public StopImageProcessingCommand(IProcessingEngine processingEngine)
        {
            _processingEngine = processingEngine;
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new InvalidOperationException();
            }

            _processingEngine.Stop();
        }

        public override bool CanExecute()
        {
            return _processingEngine != null && _processingEngine.IsWorking();
        }

        public override event EventHandler CanExecuteChanged;
    }
}