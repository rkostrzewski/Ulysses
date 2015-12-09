namespace Ulysses.App.Utils
{
    public interface ICommand<in T>
    {
        void Execute(T parameter);

        bool CanExecute(T parameter);
    }
}