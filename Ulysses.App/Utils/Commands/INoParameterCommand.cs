namespace Ulysses.App.Utils.Commands
{
    public interface INoParameterCommand
    {
        void Execute();

        bool CanExecute();
    }
}