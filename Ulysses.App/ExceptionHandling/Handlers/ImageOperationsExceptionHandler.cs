using System;
using Ulysses.Core.Exceptions;
using Ulysses.ProcessingAlgorithms.Exceptions;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal class ImageOperationsExceptionHandler : IInternalExceptionHandling
    {
        public ExceptionHandlingResolution TryHandleException(Exception exception)
        {
            if (exception.GetType() == typeof(ImageModelMismatchException))
            {
                const string genericImageModelMismatchException = "Tried to perform operation on Images of different size or bit depth.";
                const string specificImageModelMismatchException = " The error occured in {0}.";

                var imageModelMismatchException = (ImageModelMismatchException)exception;
                var message = genericImageModelMismatchException;

                if (imageModelMismatchException.ThrownBy != null)
                {
                    message += string.Format(specificImageModelMismatchException, imageModelMismatchException.ThrownBy.Name);
                }

                return new ExceptionHandlingResolution(true, message);
            }

            return new ExceptionHandlingResolution(false, string.Empty);
        }
    }
}