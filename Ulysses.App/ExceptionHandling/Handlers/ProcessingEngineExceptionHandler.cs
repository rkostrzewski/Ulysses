using System;
using Ulysses.ProcessingEngine.Exceptions;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal class ProcessingEngineExceptionHandler : IInternalExceptionHandling
    {
        public ExceptionHandlingResolution TryHandleException(Exception exception)
        {
            if (exception.GetType() == typeof (InvalidEngineStateException))
            {
                const string invalidEngineStateExceptionMessage = "Processing Engine is invalid state and cannot perform the operation. Please re-build Processing Chain.";
                return new ExceptionHandlingResolution(true, invalidEngineStateExceptionMessage);
            }

            return new ExceptionHandlingResolution(false, string.Empty);
        }
    }
}