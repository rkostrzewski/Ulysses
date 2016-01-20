using System.Threading;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingEngine.ProcessingEngine.Synchronization
{
    internal class AsyncProcessingMediator
    {
        private static readonly ReaderWriterLockSlim SlimLock = new ReaderWriterLockSlim();
        private Image _acquiredImage;
        private ImageProcessingStatus _imageProcessingStatus = ImageProcessingStatus.NoImageProcessed;

        public void SetAquiredImageAndImageAddedStatus(Image image)
        {
            SlimLock.EnterWriteLock();
            _acquiredImage = image;
            _imageProcessingStatus = ImageProcessingStatus.ImagePendingToBeProcessed;
            SlimLock.ExitWriteLock();
        }

        public Image GetAcquiredImageAndSetImageProcessedStatus()
        {
            SlimLock.EnterWriteLock();
            var image = _acquiredImage;
            _imageProcessingStatus = ImageProcessingStatus.ImageIsProcessed;
            SlimLock.ExitWriteLock();

            return image;
        }

        public ImageProcessingStatus GetImageProcessingStatus()
        {
            SlimLock.EnterReadLock();
            var imageProcessingStatus = _imageProcessingStatus;
            SlimLock.ExitReadLock();

            return imageProcessingStatus;
        }

        public void SetImageProcessingStatus(ImageProcessingStatus imageProcessingStatus)
        {
            SlimLock.EnterWriteLock();
            if (_imageProcessingStatus != ImageProcessingStatus.ImageProviderStopped)
            {
                _imageProcessingStatus = imageProcessingStatus;
            }
            SlimLock.ExitWriteLock();
        }

        public void Reset()
        {
            SlimLock.EnterWriteLock();
            _imageProcessingStatus = ImageProcessingStatus.NoImageProcessed;
            _acquiredImage = null;
            SlimLock.ExitWriteLock();
        }
    }
}