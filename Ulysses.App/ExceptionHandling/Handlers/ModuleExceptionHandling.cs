using System;
using Ulysses.App.Core.Exceptions;
using Ulysses.ProcessingAlgorithms.Exceptions;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal class ModuleExceptionHandling : IInternalExceptionHandling
    {
        public ExceptionHandlingResolution TryHandleException(Exception exception)
        {
            if (exception.GetType() == typeof(CannotExecuteCommandException))
            {
                const string cannotExecuteCommandExceptionMessage = "Current state of the application does not allow to perform the action. Please try again.";
                
                return new ExceptionHandlingResolution(true, cannotExecuteCommandExceptionMessage);
            }

            if (exception.GetType() == typeof(InvalidNonUniformityModelSourceFormatException))
            {
                const string invalidNonUniformityModelSourceFormatException = "Provided Non Uniformity Model is of incorrect format.";

                return new ExceptionHandlingResolution(true, invalidNonUniformityModelSourceFormatException);
            }

            return new ExceptionHandlingResolution(false, string.Empty);
        }
    }
}