using System.Threading.Tasks;

namespace Ulysses.ProcessingEngine
{
    public interface IProcessingEngine
    {
        Task Start();

        Task Stop();

        bool IsWorking();
    }
}