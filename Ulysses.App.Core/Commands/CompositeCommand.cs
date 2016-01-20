using System;
using System.Windows.Input;
using Prism.Commands;

namespace Ulysses.App.Core.Commands
{
    public class CompositeCommand<T> : CompositeCommand, ICompositeCommand<T>
    {
        public void Execute(T parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException();
            }

            base.Execute(parameter);
        }

        public bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter);
        }

        public void RegisterCommand(ICommand<T> command)
        {
            base.RegisterCommand(command);
        }

        public void UnRegisterCommand(ICommand<T> command)
        {
            base.RegisterCommand(command);
        }

        public override void RegisterCommand(ICommand command)
        {
            base.RegisterCommand((ICommand<T>)command);
        }

        // ReSharper disable once IdentifierTypo
        public override void UnregisterCommand(ICommand command)
        {
            base.UnregisterCommand((ICommand<T>)command);
        }
    }
}