using System;
using System.Threading.Tasks;

namespace Ulysses.App.ExceptionHandling
{
    public interface IExceptionHandler
    {
        Task HandleException(Exception exception);
    }
}