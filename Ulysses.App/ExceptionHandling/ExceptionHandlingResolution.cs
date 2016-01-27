namespace Ulysses.App.ExceptionHandling
{
    internal class ExceptionHandlingResolution
    {
        public ExceptionHandlingResolution(bool isResolved, string message)
        {
            IsResolved = isResolved;
            Message = message;
        }

        internal bool IsResolved { get; }

        internal string Message { get; }
    }
}