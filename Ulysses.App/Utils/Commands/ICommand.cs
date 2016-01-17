using System.Windows.Input;

namespace Ulysses.App.Utils.Commands
{
    public interface ICommand<in T> : ICommand
    {
        void Execute(T parameter);

        bool CanExecute(T parameter);
    }
}