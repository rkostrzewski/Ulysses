using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.DesignerCanvas;

namespace Ulysses.App.Controls.Workflows.Items.Thumbs
{
    public class DragThumb : Thumb
    {
        private WorkflowDesignerCanvas _canvas;
        private WorkflowItem _draggedItem;

        public DragThumb()
        {
            DragStarted += OnDragStarted;
            DragDelta += OnDragDelta;
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            _draggedItem = DataContext as WorkflowItem;
            _canvas = null;

            if (_draggedItem != null)
            {
                _canvas = VisualTreeHelper.GetParent(_draggedItem) as WorkflowDesignerCanvas;
            }
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_canvas == null || !_draggedItem.IsSelected)
            {
                return;
            }

            var minLeft = double.MaxValue;
            var minTop = double.MaxValue;

            foreach (var item in _canvas.SelectedItems.OfType<WorkflowItem>())
            {
                minLeft = Math.Min(Canvas.GetLeft(item), minLeft);
                minTop = Math.Min(Canvas.GetTop(item), minTop);
            }

            var horizontalChange = Math.Max(-minLeft, e.HorizontalChange);
            var verticalChange = Math.Max(-minTop, e.VerticalChange);

            foreach (var item in _canvas.SelectedItems.OfType<WorkflowItem>())
            {
                Canvas.SetLeft(item, Canvas.GetLeft(item) + horizontalChange);
                Canvas.SetTop(item, Canvas.GetTop(item) + verticalChange);
            }

            _canvas.InvalidateMeasure();
            e.Handled = true;
        }
    }
}