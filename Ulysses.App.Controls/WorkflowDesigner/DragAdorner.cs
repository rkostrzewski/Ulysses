using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ulysses.App.Controls.WorkflowDesigner
{
    public class DragAdorner : Adorner
    {
        private const int ChildCount = 1;
        private readonly Rectangle _child;
        private GeneralTransform _translateTransform;

        public DragAdorner(UIElement adornedElement, Size size, Brush brush) : base(adornedElement)
        {
            var rect = new Rectangle { Fill = brush, Width = size.Width, Height = size.Height, IsHitTestVisible = false };

            _child = rect;
        }

        protected override int VisualChildrenCount => ChildCount;

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var result = new GeneralTransformGroup();
            var baseTransform = base.GetDesiredTransform(transform);
            if (baseTransform != null)
            {
                result.Children.Add(baseTransform);
            }

            result.Children.Add(_translateTransform);
            return result;
        }

        public void SetTransformOffsets(double left, double top)
        {
            _translateTransform = new TranslateTransform(left, top);
            UpdateLocation();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }

        private void UpdateLocation()
        {
            var adornerLayer = Parent as AdornerLayer;
            adornerLayer?.Update(AdornedElement);
        }
    }
}