using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ulysses.App.Controls.DragAndDropExtension.Extensions
{
    public static class ItemsControlExtensions
    {
        private const string CanSelectMultipleItemsPropertyName = "CanSelectMultipleItems";
        private const string OrientationPropertyName = "Orientation";

        public static bool CanSelectMultipleItems(this ItemsControl itemsControl)
        {
            var selector = itemsControl as MultiSelector;
            if (selector != null)
            {
                return (bool)selector.GetType().GetProperty(CanSelectMultipleItemsPropertyName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(selector, null);
            }

            var box = itemsControl as ListBox;
            if (box != null)
            {
                return box.SelectionMode != SelectionMode.Single;
            }

            return false;
        }

        public static UIElement GetItemContainer(this ItemsControl itemsControl, UIElement child)
        {
            var itemType = GetItemContainerType(itemsControl);

            if (itemType != null)
            {
                return (UIElement)child.GetVisualAncestor(itemType);
            }

            return null;
        }

        public static UIElement GetItemContainerAt(this ItemsControl itemsControl, Point position)
        {
            var inputElement = itemsControl.InputHitTest(position);
            var uiElement = inputElement as UIElement;

            return uiElement != null ? GetItemContainer(itemsControl, uiElement) : null;
        }

        public static Type GetItemContainerType(this ItemsControl itemsControl)
        {
            if (itemsControl.Items.Count <= 0)
            {
                return null;
            }

            var itemsPresenters = itemsControl.GetVisualDescendents<ItemsPresenter>();

            return
                itemsPresenters.Select(ip => VisualTreeHelper.GetChild(ip, 0))
                               .Select(p => VisualTreeHelper.GetChild(p, 0))
                               .Where(ic => ic != null)
                               .Where(ic => itemsControl.ItemContainerGenerator.IndexFromContainer(ic) != -1)
                               .Select(ic => ic.GetType())
                               .FirstOrDefault();
        }

        public static Orientation GetItemsPanelOrientation(this ItemsControl itemsControl)
        {
            var itemsPresenter = itemsControl.GetVisualDescendent<ItemsPresenter>();
            var itemsPanel = VisualTreeHelper.GetChild(itemsPresenter, 0);
            var orientationProperty = itemsPanel.GetType().GetProperty(OrientationPropertyName, typeof (Orientation));

            if (orientationProperty != null)
            {
                return (Orientation)orientationProperty.GetValue(itemsPanel, null);
            }

            return Orientation.Vertical;
        }

        public static IEnumerable GetSelectedItems(this ItemsControl itemsControl)
        {
            var multiSelector = itemsControl as MultiSelector;
            if (multiSelector != null)
            {
                return multiSelector.SelectedItems;
            }

            var listBox = itemsControl as ListBox;
            if (listBox != null)
            {
                if (listBox.SelectionMode == SelectionMode.Single)
                {
                    return Enumerable.Repeat(listBox.SelectedItem, 1);
                }

                return listBox.SelectedItems;
            }

            var treeView = itemsControl as TreeView;
            if (treeView != null)
            {
                return Enumerable.Repeat(treeView.SelectedItem, 1);
            }

            var selector = itemsControl as Selector;
            return selector != null ? Enumerable.Repeat(selector.SelectedItem, 1) : Enumerable.Empty<object>();
        }
    }
}