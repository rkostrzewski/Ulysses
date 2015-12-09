using System.Threading;
using System.Threading.Tasks;
using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.ProcessingEngineStrategies.Synchronization;

namespace Ulysses.ProcessingEngine.ProcessingEngineStrategies
{
    public class AsyncProcessingStrategy : IProcessingStrategy
    {
        private readonly IImageAcquisitorStrategy _imageAcquisitor;
        private readonly IImageProcessingChain _imageProcessingChain;
        private readonly AsyncProcessingMediator _mediator;
        private readonly ISetOutputImageCommand _setOutputImageCommand;
        private volatile CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public AsyncProcessingStrategy(IImageAcquisitorStrategy imageAcquisitor, IImageProcessingChain imageProcessingChain, ISetOutputImageCommand setOutputImageCommand)
        {
            _imageAcquisitor = imageAcquisitor;
            _imageProcessingChain = imageProcessingChain;
            _setOutputImageCommand = setOutputImageCommand;
            _mediator = new AsyncProcessingMediator();
        }

        public Task Start()
        {
            if (_task != null && !_task.IsCompleted)
            {
                throw new InvalidEngineStateException();
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _mediator.Reset();

            var imageAcquireTask = Task.Factory.StartNew(AcquireImageWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            var imageProcessingTask = Task.Factory.StartNew(ProcessImageWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            _task = Task.WhenAll(imageAcquireTask, imageProcessingTask);

            return _task;
        }

        public async Task Stop()
        {
            if (_cancellationTokenSource == null || _task == null)
            {
                throw new InvalidEngineStateException();
            }

            _cancellationTokenSource.Cancel();
            _mediator.Reset();

            await _task;
        }

        private void ProcessImageWork()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var imageProcessingStatus = _mediator.GetImageProcessingStatus();
                if (imageProcessingStatus == ImageProcessingStatus.ImageAcquisitionStopped)
                {
                    break;
                }

                if (imageProcessingStatus == ImageProcessingStatus.ImagePendingToBeProcessed)
                {
                    GetAcquiredImageAndProcessIt(_mediator);
                }

                Thread.SpinWait(1);
            }
        }

        private void AcquireImageWork()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                Image image;
                if (!_imageAcquisitor.TryToObtainImage(out image))
                {
                    break;
                }

                SetAcquiredImage(_mediator, image);

                Thread.SpinWait(1);
            }

            _mediator.SetImageProcessingStatus(ImageProcessingStatus.ImageAcquisitionStopped);
        }

        private static void SetAcquiredImage(AsyncProcessingMediator mediator, Image image)
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