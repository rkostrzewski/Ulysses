using System;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal interface IInternalExceptionHandling
    {
        ExceptionHandlingResolution TryHandleException(Exception exception);
    }
}