namespace Ulysses.App.Core.Commands
{
    public interface INoParameterCommand
    {
        void Execute();

        bool CanExecute();
    }
}