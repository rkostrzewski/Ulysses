using System.Windows.Input;

namespace Ulysses.App.Core.Commands
{
    public interface ICommand<in T> : ICommand
    {
        void Execute(T parameter);

        bool CanExecute(T parameter);
    }
}