using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.DesignerCanvas;
using Ulysses.App.Controls.Workflows.Items;

namespace Ulysses.App.Controls.Workflows.Adorners
{
    public class RubberBandAdorner : Adorner
    {
        private readonly WorkflowDesignerCanvas _designerCanvas;
        private readonly Pen _rubberBandPen;
        private Point? _endPoint;
        private Point? _startPoint;

        public RubberBandAdorner(WorkflowDesignerCanvas designerCanvas, Point dragStartPoint) : base(designerCanvas)
        {
            _designerCanvas = designerCanvas;
            _startPoint = dragStartPoint;
            _rubberBandPen = new Pen(Brushes.LightSlateGray, 1) { DashStyle = new DashStyle(new double[] { 2 }, 1) };
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            e.Handled = true;

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                if (IsMouseCaptured)
                {
                    ReleaseMouseCapture();
                }

                return;
            }

            if (!IsMouseCaptured)
            {
                CaptureMouse();
            }

            _endPoint = e.GetPosition(this);
            UpdateSelection();
            InvalidateVisual();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }

            var adornerLayer = Parent as AdornerLayer;
            adornerLayer?.Remove(this);
            e.Handled = true;
        }

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);

            context.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));

            if (_startPoint.HasValue && _endPoint.HasValue)
            {
                context.DrawRectangle(Brushes.Transparent, _rubberBandPen, new Rect(_startPoint.Value, _endPoint.Value));
            }
        }

        private void UpdateSelection()
        {
            if (!_startPoint.HasValue || !_endPoint.HasValue)
            {
                return;
            }

            _designerCanvas.DeselectAll();

            var rubberBand = new Rect(_startPoint.Value, _endPoint.Value);

            foreach (var item in _designerCanvas.Children.OfType<WorkflowItem>().Where(i =>
            {
                var itemRect = VisualTreeHelper.GetDescendantBounds(i);
                var itemBounds = i.TransformToAncestor(_designerCanvas).TransformBounds(itemRect);
                return rubberBand.IntersectsWith(itemBounds);
            }))
            {
                item.IsSelected = true;
            }
        }
    }
}