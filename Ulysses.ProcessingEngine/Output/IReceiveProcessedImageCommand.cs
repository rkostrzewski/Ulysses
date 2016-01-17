using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine.Output
{
    public interface IReceiveProcessedImageCommand
    {
        void Execute(Image parameter);
    }
}