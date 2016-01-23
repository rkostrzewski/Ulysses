using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Ulysses.App.Controls.DragAndDropExtension.Extensions;

namespace Ulysses.App.Controls.DragAndDropExtension
{
    public class DropInfo : IDropInfo
    {
        public DropInfo(object sender, DragEventArgs e, IDragInfo dragInfo, string dataFormat)
        {
            Data = e.Data.GetDataPresent(dataFormat) ? e.Data.GetData(dataFormat) : e.Data;
            DragInfo = dragInfo;
            VisualTarget = sender as UIElement;

            InitializeFromSender(sender, e);
        }

        public object Data { get; private set; }
        public IDragInfo DragInfo { get; private set; }
        public Type DropTargetAdorner { get; set; }
        public DragDropEffects Effects { get; set; }
        public int InsertIndex { get; private set; }
        public IEnumerable TargetCollection { get; private set; }
        public object TargetItem { get; private set; }
        public UIElement VisualTarget { get; private set; }
        public UIElement VisualTargetItem { get; private set; }
        public Orientation VisualTargetOrientation { get; private set; }

        private void InitializeFromSender(object sender, DragEventArgs e)
        {
            var control = sender as ItemsControl;
            if (control == null)
            {
                return;
            }

            PopulateInfoFromControl(control, e);
        }

        private void PopulateInfoFromControl(ItemsControl control, DragEventArgs e)
        {
            var itemsControl = control;
            var item = itemsControl.GetItemContainerAt(e.GetPosition(itemsControl));

            VisualTargetOrientation = itemsControl.GetItemsPanelOrientation();
            VisualTargetItem = item;

            if (item == null)
            {
                TargetCollection = itemsControl.ItemsSource ?? itemsControl.Items;
                InsertIndex = itemsControl.Items.Count;
                return;
            }

            var itemParent = ItemsControl.ItemsControlFromItemContainer(item);

            InsertIndex = itemParent.ItemContainerGenerator.IndexFromContainer(item);
            TargetCollection = itemParent.ItemsSource ?? itemParent.Items;
            TargetItem = itemParent.ItemContainerGenerator.ItemFromContainer(item);

            if (VisualTargetOrientation == Orientation.Vertical)
            {
                if (e.GetPosition(item).Y > item.RenderSize.Height / 2)
                {
                    InsertIndex++;
                }
                return;
            }

            if (e.GetPosition(item).X > item.RenderSize.Width / 2)
            {
                InsertIndex++;
            }
        }
    }
}