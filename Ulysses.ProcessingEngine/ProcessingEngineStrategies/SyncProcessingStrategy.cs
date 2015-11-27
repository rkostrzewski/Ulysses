using System;
using System.Threading;
using System.Threading.Tasks;
using Ulysses.Core;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition;

namespace Ulysses.ProcessingEngine.ProcessingEngineStrategies
{
    public class SyncProcessingStrategy : IProcessingStrategy
    {
        private readonly IImageAcquisitorStrategy _imageAcquisitor;
        private readonly IImageProcessingChain _imageProcessingChain;
        private readonly ISetOutputImageCommand _imageOutputNotifier;
        private CancellationTokenSource _cancellationTokenSource;
        private volatile bool _shouldWork;

        public SyncProcessingStrategy(IImageAcquisitorStrategy imageAcquisitor, IImageProcessingChain imageProcessingChain, ISetOutputImageCommand imageOutputNotifier)
        {
            _imageAcquisitor = imageAcquisitor;
            _imageProcessingChain = imageProcessingChain;
            _imageOutputNotifier = imageOutputNotifier;
        }
        
        public Task Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _shouldWork = true;
            return Task.Factory.StartNew(DoWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        public void Stop()
        {
            _shouldWork = false;
            _cancellationTokenSource.Cancel();
        }

        private void DoWork()
        {
            while (_shouldWork)
            {
                Image image;
                if (!_imageAcquisitor.TryToObtainImage(out image))
                {
                    return;
                }

                if (!_shouldWork)
                {
                    return;
                }

                image = _imageProcessingChain.ProcessImage(image);
                _imageOutputNotifier.SetOuputImageAsync(image);
            }
        }
    }
}