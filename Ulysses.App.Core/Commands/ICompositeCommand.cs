using System.Windows.Input;

namespace Ulysses.App.Core.Commands
{
    public interface ICompositeCommand<in T> : ICommand<T>
    {
        void RegisterCommand(ICommand command);

        // ReSharper disable once IdentifierTypo
        void UnregisterCommand(ICommand command);
    }
}
