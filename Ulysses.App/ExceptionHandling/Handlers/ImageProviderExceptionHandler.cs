using System;
using System.IO;
using System.Net.Sockets;
using Ulysses.ImageProviders.Exception;

namespace Ulysses.App.ExceptionHandling.Handlers
{
    internal class ImageProviderExceptionHandler : IInternalExceptionHandling
    {
        public ExceptionHandlingResolution TryHandleException(Exception exception)
        {
            if (exception.GetType() == typeof (SocketException))
            {
                const string socketExceptionMessage = "Connection to the Camera could not be established.";
                return new ExceptionHandlingResolution(true, socketExceptionMessage);
            }

            if (exception.GetType() == typeof (IOException))
            {
                const string ioExceptionMessage = "Could not access file on disk.";
                return new ExceptionHandlingResolution(true, ioExceptionMessage);
            }

            if (exception.GetType() == typeof (NotSupportedImageSizeForCameraProviderException))
            {
                const string notSupportedImageSizeForCameraProviderException = "Provided image size is not support for Camera Provider.";
                return new ExceptionHandlingResolution(true, notSupportedImageSizeForCameraProviderException);
            }

            return new ExceptionHandlingResolution(false, string.Empty);
        }
    }
}