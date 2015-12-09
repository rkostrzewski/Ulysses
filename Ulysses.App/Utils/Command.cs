using System;
using System.Windows.Input;

namespace Ulysses.App.Utils
{
    public abstract class Command<T> : ICommand<T>, ICommand
    {
        public abstract event EventHandler CanExecuteChanged;
        
        public abstract bool CanExecute(T parameter);

        public abstract void Execute(T parameter);

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
}