using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ulysses.App.Controls.DragAndDropExtension.Adorners
{
    public class DragAdorner : Adorner
    {
        private readonly AdornerLayer _adornerLayer;
        private readonly UIElement _adornment;
        private Point _mousePosition;

        public DragAdorner(UIElement adornedElement, UIElement adornment) : base(adornedElement)
        {
            _adornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            _adornerLayer.Add(this);
            _adornment = adornment;
            IsHitTestVisible = false;
        }

        public Point MousePosition
        {
            get
            {
                return _mousePosition;
            }
            set
            {
                if (_mousePosition == value)
                {
                    return;
                }

                _mousePosition = value;
                _adornerLayer.Update(AdornedElement);
            }
        }

        protected override int VisualChildrenCount => 1;

        public void Detach()
        {
            _adornerLayer.Remove(this);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _adornment.Arrange(new Rect(finalSize));
            return finalSize;
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            const int padding = 4;

            var result = new GeneralTransformGroup();
            var baseTransform = base.GetDesiredTransform(transform);
            if (baseTransform != null)
            {
                result.Children.Add(baseTransform);
            }

            result.Children.Add(new TranslateTransform(MousePosition.X - padding, MousePosition.Y - 4));

            return result;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _adornment;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _adornment.Measure(constraint);
            return _adornment.DesiredSize;
        }
    }
}