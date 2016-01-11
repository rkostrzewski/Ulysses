using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ulysses.App.Controls.Workflows.Items.Adorners
{
    public class SelectedItemAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly SelectedItemChrome _chrome;

        public SelectedItemAdorner(UIElement workflowItem) : base(workflowItem)
        {
            SnapsToDevicePixels = true;
            _chrome = new SelectedItemChrome { DataContext = workflowItem };
            _visuals = new VisualCollection(this) { _chrome };
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            _chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        protected override int VisualChildrenCount => _visuals.Count;
    }
}