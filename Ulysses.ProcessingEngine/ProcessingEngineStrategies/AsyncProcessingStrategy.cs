using System.Threading;
using System.Threading.Tasks;
using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.ProcessingEngineStrategies.Synchronization;

namespace Ulysses.ProcessingEngine.ProcessingEngineStrategies
{
    public class AsyncProcessingStrategy : IProcessingStrategy
    {
        private readonly IImageAcquisitorStrategy _imageAcquisitor;
        private readonly IImageProcessingChain _imageProcessingChain;
        private readonly ISetOutputImageCommand _setOutputImageCommand;
        private CancellationTokenSource _cancellationTokenSource;
        private static readonly AsyncProcessingMediator Mediator = new AsyncProcessingMediator();

        private volatile bool _shouldWork;

        public AsyncProcessingStrategy(IImageAcquisitorStrategy imageAcquisitor,
                                       IImageProcessingChain imageProcessingChain,
                                       ISetOutputImageCommand setOutputImageCommand)
        {
            _imageAcquisitor = imageAcquisitor;
            _imageProcessingChain = imageProcessingChain;
            _setOutputImageCommand = setOutputImageCommand;
        }

        public Task Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _shouldWork = true;
            Mediator.Reset();

            var imageAcquireTask = Task.Factory.StartNew(AcquireImageWork,
                _cancellationTokenSource.Token,
                                                         TaskCreationOptions.LongRunning,
                                                         TaskScheduler.Default);
            var imageProcessingTask = Task.Factory.StartNew(ProcessImageWork,
                _cancellationTokenSource.Token,
                                                            TaskCreationOptions.LongRunning,
                                                            TaskScheduler.Default);

            return Task.WhenAll(imageAcquireTask, imageProcessingTask);
        }

        public void Stop()
        {

            _shouldWork = false;
            _cancellationTokenSource.Cancel();
            Mediator.Reset();
        }

        private void ProcessImageWork()
        {
            while (_shouldWork)
            {
                var imageProcessingStatus = Mediator.GetImageProcessingStatus();
                if (imageProcessingStatus == ImageProcessingStatus.ImageAcquisitionStopped)
                {
                    break;
                }

                if (imageProcessingStatus == ImageProcessingStatus.ImagePendingToBeProcessed)
                {
                    GetAcquiredImageAndProcessIt(Mediator);
                }

                Thread.SpinWait(1);
            }
        }

        private void AcquireImageWork()
        {
            while (_shouldWork)
            {
                Image image;
                if (!_imageAcquisitor.TryToObtainImage(out image))
                {
                    break;
                }

                SetAcquiredImage(Mediator, image);

                Thread.SpinWait(1);
            }

            Mediator.SetImageProcessingStatus(ImageProcessingStatus.ImageAcquisitionStopped);
        }

        private void SetAcquiredImage(AsyncProcessingMediator mediator, Image image)
        {
            var processingStatus = mediator.GetImageProcessingStatus();

            if (processingStatus == ImageProcessingStatus.NoImageProcessed)
            {
                mediator.SetAquiredImageAndImageAddedStatus(image);
            }
        }

        private void GetAcquiredImageAndProcessIt(AsyncProcessingMediator mediator)
        {
            var image = mediator.GetAcquiredImageAndSetImageProcessedStatus();

            try
            {
                var processedImage = _imageProcessingChain.ProcessImage(image);
                _setOutputImageCommand.SetOuputImageAsync(processedImage);
            }
            finally
            {
                mediator.SetImageProcessingStatus(ImageProcessingStatus.NoImageProcessed);
            }
        }
    }
}

