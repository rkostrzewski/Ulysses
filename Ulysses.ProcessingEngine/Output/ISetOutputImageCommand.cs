using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine.Output
{
    public interface ISetOutputImageCommand
    {
        void Execute(Image parameter);
    }
}