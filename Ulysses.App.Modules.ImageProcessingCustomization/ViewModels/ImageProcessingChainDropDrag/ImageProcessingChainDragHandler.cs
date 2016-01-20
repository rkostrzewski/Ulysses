using System.Linq;
using Ulysses.App.Controls.DragAndDropExtension;
using Ulysses.App.Controls.DragAndDropExtension.Handlers;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag
{
    public class ImageProcessingChainDragHandler : DefaultDragHandler, IImageProcessingChainDragHandler
    {
        public override void StartDrag(DragInfo dragInfo)
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