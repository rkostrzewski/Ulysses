using System.Windows;
using System.Windows.Media;

namespace Ulysses.App.Controls.DragAndDropExtension.Adorners
{
    public class DropTargetHighlightAdorner : DropTargetAdorner
    {
        public DropTargetHighlightAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (DropInfo.VisualTargetItem == null)
            {
                return;
            }

            var location = DropInfo.VisualTargetItem.TranslatePoint(new Point(), AdornedElement);
            var size = VisualTreeHelper.GetDescendantBounds(DropInfo.VisualTargetItem).Size;
            var rectangle = new Rect(location, size);

            const int radius = 2;
            drawingContext.DrawRoundedRectangle(null, new Pen(Brushes.Gray, radius), rectangle, 2, 2);
        }
    }
}