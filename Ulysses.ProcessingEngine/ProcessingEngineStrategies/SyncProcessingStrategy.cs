using System;
using System.Threading;
using System.Threading.Tasks;
using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;
using Ulysses.ProcessingEngine.Exceptions;

namespace Ulysses.ProcessingEngine.ProcessingEngineStrategies
{
    public class SyncProcessingStrategy : IProcessingStrategy
    {
        private readonly IImageAcquisitorStrategy _imageAcquisitor;
        private readonly IImageProcessingChain _imageProcessingChain;
        private readonly ISetOutputImageCommand _imageOutputNotifier;
        private volatile CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public SyncProcessingStrategy(IImageAcquisitorStrategy imageAcquisitor, IImageProcessingChain imageProcessingChain, ISetOutputImageCommand imageOutputNotifier)
        {
            _imageAcquisitor = imageAcquisitor;
            _imageProcessingChain = imageProcessingChain;
            _imageOutputNotifier = imageOutputNotifier;
        }
        
        public Task Start()
        {
            if (_task != null && !_task.IsCompleted)
            {
                throw new InvalidEngineStateException();
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _task = Task.Factory.StartNew(DoWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
            return _task;
        }

        public async Task Stop()
        {
            if (_cancellationTokenSource == null || _task == null)
            {
                throw new InvalidEngineStateException();
            }

            _cancellationTokenSource.Cancel();
            await _task;
        }

        private void DoWork()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                Image image;
                if (!_imageAcquisitor.TryToObtainImage(out image))
                {
                    return;
                }

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                image = _imageProcessingChain.ProcessImage(image);
                _imageOutputNotifier.SetOuputImageAsync(image);
            }
        }
    }
}