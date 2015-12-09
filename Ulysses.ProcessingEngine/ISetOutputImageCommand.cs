using System.Threading.Tasks;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine
{
    public interface ISetOutputImageCommand
    {
        void Execute(Image image);
    }
}