namespace Ulysses.ProcessingEngine.ProcessingEngine.Synchronization
{
    internal enum ImageProcessingStatus
    {
        NoImageProcessed = 0,
        ImagePendingToBeProcessed,
        ImageIsProcessed,
        ImageProviderStopped
    }
}