namespace Ulysses.App.Core.Commands
{
    public interface ICompositeCommand<T> : ICommand<T>
    {
        void RegisterCommand(ICommand<T> command);

        void UnRegisterCommand(ICommand<T> command);
    }
}
