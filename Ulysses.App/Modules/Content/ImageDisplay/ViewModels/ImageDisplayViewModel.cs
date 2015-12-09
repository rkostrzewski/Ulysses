using Ulysses.App.Modules.Content.ImageDisplay.Commands;
using Ulysses.ProcessingEngine;

namespace Ulysses.App.Modules.Content.ImageDisplay.ViewModels
{
    public class ImageDisplayViewModel : IImageDisplayViewModel
    {
        private readonly IProcessingEngine _processingEngine;

        public ImageDisplayViewModel(IProcessingEngine processingEngine)
        {
            _processingEngine = processingEngine;
        }

        public IStartImageProcessingCommand StartImageProcessingCommand { get; }
        public IStopImageProcessingCommand StopImageProcessingCommand { get; }
    }
}
