using System.Threading.Tasks;

namespace Ulysses.ProcessingEngine.ProcessingEngine
{
    public interface IProcessingEngine
    {
        Task Start();

        Task Stop();

        bool IsWorking();
    }
}