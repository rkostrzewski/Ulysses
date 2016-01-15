using System;
using System.Windows.Input;

namespace Ulysses.App.Utils.Commands
{
    public abstract class NoParameterCommand : INoParameterCommand, ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public abstract bool CanExecute();

        public abstract void Execute();
    }
}