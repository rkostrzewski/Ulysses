using System.Threading;
using System.Threading.Tasks;
using Ulysses.Core.Models;
using Ulysses.ImageProviders;
using Ulysses.ProcessingAlgorithms;
using Ulysses.ProcessingEngine.Exceptions;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.ProcessingEngine.ProcessingEngine
{
    public class SyncProcessingEngine : IProcessingEngine
    {
        private readonly IReceiveProcessedImageCommand _imageOutputNotifier;
        private readonly IImageProcessingChain _imageProcessingChain;
        private readonly IImageProvider _imageProvider;
        private volatile CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public SyncProcessingEngine(IImageProvider imageProvider, IImageProcessingChain imageProcessingChain, IReceiveProcessedImageCommand receiveProcessedImageCommand)
        {
            _imageProvider = imageProvider;
            _imageProcessingChain = imageProcessingChain;
            _imageOutputNotifier = receiveProcessedImageCommand;
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

        public bool IsWorking()
        {
            return _task != null && !_task.IsCompleted;
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
                if (!_imageProvider.TryToObtainImage(out image))
                {
                    return;
                }

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                image = _imageProcessingChain.ProcessImage(image);
                _imageOutputNotifier.Execute(image);
            }
        }

        public void Dispose()
        {
            _imageProvider.Dispose();
        }
    }
}