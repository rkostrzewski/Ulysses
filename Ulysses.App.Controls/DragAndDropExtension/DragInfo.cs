using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ulysses.App.Controls.DragAndDropExtension.Extensions;

namespace Ulysses.App.Controls.DragAndDropExtension
{
    public class DragInfo : IDragInfo
    {
        public DragInfo(object sender, MouseButtonEventArgs e)
        {
            DragStartPosition = e.GetPosition(null);
            Effects = DragDropEffects.None;
            MouseButton = e.ChangedButton;
            VisualSource = sender as UIElement;

            InitializeFromSender(sender, e);
        }

        public object Data { get; set; }
        public Point DragStartPosition { get; }
        public DragDropEffects Effects { get; set; }
        public MouseButton MouseButton { get; }
        public IEnumerable SourceCollection { get; private set; }
        public object SourceItem { get; private set; }
        public IEnumerable SourceItems { get; set; }
        public UIElement VisualSource { get; }
        public UIElement VisualSourceItem { get; private set; }

        private void InitializeFromSender(object sender, RoutedEventArgs e)
        {
            var control = sender as ItemsControl;
            if (control != null)
            {
                PopulateInfoFromItemControl(e, control);
            }

            if (SourceItems == null)
            {
                SourceItems = Enumerable.Empty<object>();
            }
        }

        private void PopulateInfoFromItemControl(RoutedEventArgs e, ItemsControl control)
        {
            var itemsControl = control;
            var item = itemsControl.GetItemContainer((UIElement)e.OriginalSource);

            if (item == null)
            {
                SourceCollection = itemsControl.ItemsSource ?? itemsControl.Items;
                return;
            }

            var itemParent = ItemsControl.ItemsControlFromItemContainer(item);

            SourceCollection = itemParent.ItemsSource ?? itemParent.Items;
            SourceItem = itemParent.ItemContainerGenerator.ItemFromContainer(item);
            SourceItems = itemsControl.GetSelectedItems();

            if (SourceItems.Cast<object>().Count() <= 1)
            {
                SourceItems = Enumerable.Repeat(SourceItem, 1);
            }

            VisualSourceItem = item;
        }
    }
}