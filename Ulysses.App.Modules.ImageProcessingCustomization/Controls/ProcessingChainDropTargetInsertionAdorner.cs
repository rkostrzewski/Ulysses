using System.Windows;
using Ulysses.App.Controls.DragAndDropExtension.Adorners;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Controls
{
    public class ProcessingChainDropTargetInsertionAdorner : DropTargetInsertionAdorner
    {
        static ProcessingChainDropTargetInsertionAdorner()
        {
            ScaleFactor = 0.3d;
        }

        public ProcessingChainDropTargetInsertionAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }
    }
}
