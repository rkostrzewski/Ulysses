using System.Threading.Tasks;

namespace Ulysses.ProcessingEngine
{
    public interface IProcessingStrategy
    {
        Task Start();

        void Stop();
    }
}