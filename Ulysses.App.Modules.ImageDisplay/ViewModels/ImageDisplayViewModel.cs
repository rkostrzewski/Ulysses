using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Prism.Events;
using Ulysses.App.Core.Events;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.ImageDisplay.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.App.Modules.ImageDisplay.ViewModels
{
    public class ImageDisplayViewModel : NotifyPropertyChanged, IImageDisplayViewModel
    {
        private readonly IProcessingService _processingService;
        private BitmapSource _outputImage;

        public ImageDisplayViewModel(IEventAggregator eventAggregator,
                                     IProcessingService processingService,
                                     IStartImageProcessingCommand startImageProcessingCommand,
                                     IStopImageProcessingCommand stopImageProcessingCommand,
                                     ISetOutputImageCommand setOutputImageCommand,
                                     IImageConverter imageConverter)
        {
            _processingService = processingService;

            StartImageProcessingCommand = startImageProcessingCommand;
            StopImageProcessingCommand = stopImageProcessingCommand;
            SetOutputImageCommand = setOutputImageCommand;

            StartImageProcessingCommand.OnProcessingStop = () => { OnPropertyChanged(string.Empty); };
            StopImageProcessingCommand.OnProcessingStop = () => { OnPropertyChanged(string.Empty); };
            SetOutputImageCommand.OnImageUpdate = SetOutputImage;

            eventAggregator.GetEvent<ShouldUpdateProcessingEngineEvent>().Subscribe(UpdateProcessingEngine);
        }

        public ISetOutputImageCommand SetOutputImageCommand { get; }

        public BitmapSource OutputImage
        {
            get
            {
                return _outputImage;
            }
            set
            {
                _outputImage = value;
                OnPropertyChanged();
            }
        }

        public IStartImageProcessingCommand StartImageProcessingCommand { get; }

        public IStopImageProcessingCommand StopImageProcessingCommand { get; }

        private void SetOutputImage(BitmapSource bitmap)
        {
            if (ReferenceEquals(_outputImage, bitmap))
            {
                return;
            }

            var copy = bitmap.Clone();
            copy.Freeze();

            Dispatcher.CurrentDispatcher.Invoke(() => OutputImage = copy);
        }

        private void UpdateProcessingEngine(ProcessingEngineTemplate template)
        {
            template.ReceiveProcessedImageCommand = SetOutputImageCommand;

            if (StopImageProcessingCommand.CanExecute())
            {
                StopImageProcessingCommand.Execute();
            }

            _processingService.UpdateProcessingEngine(template);

            OnPropertyChanged(string.Empty);
        }
    }
}