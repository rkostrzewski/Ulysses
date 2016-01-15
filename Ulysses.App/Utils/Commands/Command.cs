using System;
using System.Windows.Input;

namespace Ulysses.App.Utils.Commands
{
    public abstract class Command<T> : ICommand<T>, ICommand
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
            return CanExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        public abstract bool CanExecute(T parameter);

        public abstract void Execute(T parameter);
    }
}