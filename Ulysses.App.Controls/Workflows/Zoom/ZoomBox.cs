using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.Templates;

namespace Ulysses.App.Controls.Workflows.Zoom
{
    [TemplatePart(Name = TemplateNames.ZoomSlider, Type = typeof(Slider))]
    public class ZoomBox : Control
    {
        private static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(nameof(ScrollViewer), typeof (ScrollViewer), typeof (ZoomBox));
        private ScaleTransform _scaleTransform;

        public ScrollViewer ScrollViewer
        {
            get
            {
                return (ScrollViewer)GetValue(ScrollViewerProperty);
            }
            set
            {
                SetValue(ScrollViewerProperty, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ScrollViewer == null)
            {
                return;
            }

            _scaleTransform = new ScaleTransform();

            var slider = (Slider)Template.FindName(TemplateNames.ZoomSlider, this);
            var canvas = (Canvas)ScrollViewer.Content;

            slider.ValueChanged += OnZoomSliderValueChanged;
            canvas.LayoutTransform = _scaleTransform;
        }

        private void OnZoomSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var scale = e.NewValue / e.OldValue;
            _scaleTransform.ScaleX *= scale;
            _scaleTransform.ScaleY *= scale;

            var halfViewportHeight = ScrollViewer.ViewportHeight / 2;
            var oldVerticalOffset = ScrollViewer.VerticalOffset;
            var newVerticalOffset = (oldVerticalOffset + halfViewportHeight) * scale - halfViewportHeight;

            var halfViewportWidth = ScrollViewer.ViewportWidth / 2;
            var oldHorizontalOffset = ScrollViewer.HorizontalOffset;
            var newHorizontalOffset = (oldHorizontalOffset + halfViewportWidth) * scale - halfViewportWidth;

            ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
        }
    }
}