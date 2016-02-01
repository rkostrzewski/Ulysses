using System;
using System.Threading.Tasks;

namespace Ulysses.ProcessingEngine.ProcessingEngine
{
    public interface IProcessingEngine : IDisposable
    {
        Task Start();

        Task Stop();

        bool IsWorking();
    }
}