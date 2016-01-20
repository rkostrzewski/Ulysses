using System;
using System.Windows.Input;

namespace Ulysses.App.Core.Commands
{
    public abstract class NoParameterCommand : INoParameterCommand, ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

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