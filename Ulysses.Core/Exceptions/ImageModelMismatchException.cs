using System;

namespace Ulysses.Core.Exceptions
{
    public class ImageModelMismatchException : Exception
    {
        public ImageModelMismatchException()
        {
            
        }

        public ImageModelMismatchException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}