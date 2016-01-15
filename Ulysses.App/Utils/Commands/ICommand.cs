namespace Ulysses.App.Utils.Commands
{
    public interface ICommand<in T>
    {
        void Execute(T parameter);

        bool CanExecute(T parameter);
    }
}