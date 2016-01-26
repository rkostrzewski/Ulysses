using System.Linq;
using Ulysses.App.Controls.DragAndDropExtension;
using Ulysses.App.Controls.DragAndDropExtension.Handlers;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop
{
    public class ProcessingChainDragHandler : DefaultDragHandler, IProcessingChainDragHandler
    {
        public override void StartDrag(IDragInfo dragInfo)
        {
            var imageProcessingAlgorithms = dragInfo.SourceItems.OfType<IImageProcessingAlgorithmTemplate>().ToList();
            dragInfo.SourceItems = imageProcessingAlgorithms;

            if (!imageProcessingAlgorithms.Any())
            {
                dragInfo.Data = null;
            }

            base.StartDrag(dragInfo);
        }
    }
}