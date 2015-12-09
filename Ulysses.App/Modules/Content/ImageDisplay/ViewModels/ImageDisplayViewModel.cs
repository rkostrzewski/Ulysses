using System.Windows.Media.Imaging;
using Ulysses.App.Modules.Content.ImageDisplay.Commands;
using Ulysses.ProcessingEngine.Output;
using Ulysses.ProcessingEngine.ProcessingEngine.Factories;

namespace Ulysses.App.Modules.Content.ImageDisplay.ViewModels
{
    public class ImageDisplayViewModel : IImageDisplayViewModel
    {
        private readonly IProcessingEngineFactory _processingEngineFactory;

        public ImageDisplayViewModel(IProcessingEngineFactory processingEngineFactory)
        {
            _processingEngineFactory = processingEngineFactory;
            StartImageProcessingCommand = new StartImageProcessingCommand(null);
            StopImageProcessingCommand = new StopImageProcessingCommand(null);
            SetOutputImageCommand = new SetOutputImageCommand(value => OutputImage = value);
        }

        private ISetOutputImageCommand SetOutputImageCommand { get; }

        public BitmapSource OutputImage { get; private set; }

        public IStartImageProcessingCommand StartImageProcessingCommand { get; }

        public IStopImageProcessingCommand StopImageProcessingCommand { get; }
    }
}