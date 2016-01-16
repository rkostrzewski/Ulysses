using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Ulysses.App.Controls.WorkflowDesigner
{
    public static class ListViewMouseExtensions
    {
        public static ListViewItem GetListViewItem(this ListView listView, int index)
        {
            return !IsItemContainerGenerated(listView) ? null : listView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }

        public static ListViewItem GetListViewItem<T>(this ListView listView, T dataItem)
        {
            return !IsItemContainerGenerated(listView) ? null : listView.ItemContainerGenerator.ContainerFromItem(dataItem) as ListViewItem;
        }

        public static bool IsItemContainerGenerated(ListView listView)
        {
            return listView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated;
        }

        public static int? GetIndexOfItemUnderCursor(this ListView listView, Func<IInputElement, Point> getRelativeMousePositionFunc)
        {
            int? index = null;

            for (var i = 0; i < listView.Items.Count; ++i)
            {
                var item = listView.GetListViewItem(i);
                if (item == null)
                {
                    continue;
                }
                var relativeMousePosition = getRelativeMousePositionFunc(item);
                if (!item.IsMouseOverVisual(relativeMousePosition))
                {
                    continue;
                }

                index = i;
                break;
            }

            return index;
        }

        public static bool IsMouseOverScrollbar(this ListView listView, Point mousePosition)
        {
            var hitTestResult = VisualTreeHelper.HitTest(listView, mousePosition);
            if (hitTestResult == null)
            {
                return false;
            }

            var dependencyObject = hitTestResult.VisualHit;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar)
                {
                    return true;
                }

                if (dependencyObject is Visual || dependencyObject is Visual3D)
                {
                    dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
                    continue;
                }

                dependencyObject = LogicalTreeHelper.GetParent(dependencyObject);
            }

            return false;
        }
    }
}