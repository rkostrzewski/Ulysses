using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Exceptions;
using Ulysses.Core.Exceptions;
using Ulysses.ProcessingAlgorithms.Exceptions;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal class ProcessingAlgorithmExceptionHandling : IInternalExceptionHandling
    {
        public ExceptionHandlingResolution TryHandleException(Exception exception)
        {
            if (exception.GetType() == typeof (AlgorithmCreationException))
            {
                const string algorithmCreationExceptionMessage = "Could not create {0} algorithm.";
                var algorithmName = ((AlgorithmCreationException)exception).AlgorithmType?.Name;
                var socketExceptionMessage = string.Format(algorithmCreationExceptionMessage, algorithmName);
                return new ExceptionHandlingResolution(true, socketExceptionMessage);
            }
            
            if (exception.GetType() == typeof(InvalidProcessingChainElementException))
            {
                const string invalidProcessingChainElementException = "Object is not a valid Processing Chain Element.";
                
                return new ExceptionHandlingResolution(true, invalidProcessingChainElementException);
            }

            if (exception.GetType() == typeof (InvalidProcessingChainElementInsertionException))
            {
                const string invalidProcessingChainElementInsertionException = "Element cannot be inserted at specified position to the Processing Chain.";

                return new ExceptionHandlingResolution(true, invalidProcessingChainElementInsertionException);
            }

            return new ExceptionHandlingResolution(false, string.Empty);
        }
    }
}