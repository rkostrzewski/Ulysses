namespace Ulysses.App.Utils
{
    public interface INoParameterCommand
    {
        void Execute();

        bool CanExecute();
    }
}