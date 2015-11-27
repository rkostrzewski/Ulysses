namespace Ulysses.ProcessingEngine.ProcessingEngineStrategies.Synchronization
{
    internal enum ImageProcessingStatus
    {
        NoImageProcessed = 0,
        ImagePendingToBeProcessed,
        ImageIsProcessed,
        ImageAcquisitionStopped
    }
}