using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Ulysses.App.Controls.DragAndDropExtension.Adorners;

namespace Ulysses.App.Controls.DragAndDropExtension.Handlers
{
    public class DefaultDropHandler : IDropTarget
    {
        public virtual void DragOver(IDropInfo dropInfo)
        {
            if (!CanAcceptData(dropInfo))
            {
                return;
            }

            dropInfo.Effects = DragDropEffects.Copy;
            dropInfo.DropTargetAdorner = typeof(DropTargetInsertionAdorner);
        }

        public virtual void Drop(IDropInfo dropInfo)
        {
            var extractedData = ExtractData(dropInfo.Data);
            var data = extractedData as object[] ?? extractedData.Cast<object>().ToArray();

            var sourceList = GetList(dropInfo.DragInfo.SourceCollection);
            var destinationList = GetList(dropInfo.TargetCollection);

            var insertIndex = PrepareSourceList(dropInfo, data, sourceList, destinationList);

            InsertIntoDestinationList(data, destinationList, insertIndex);
        }

        protected virtual void InsertIntoDestinationList(object[] data, IList destinationList, int insertIndex)
        {
            foreach (var o in data)
            {
                destinationList.Insert(insertIndex++, o);
            }
        }

        protected virtual int PrepareSourceList(IDropInfo dropInfo, object[] data, IList sourceList, IList destinationList)
        {
            var insertIndex = dropInfo.InsertIndex;

            if (!Equals(dropInfo.DragInfo.VisualSource, dropInfo.VisualTarget))
            {
                return insertIndex;
            }

            foreach (var index in data.Select(sourceList.IndexOf).Where(index => index != -1))
            {
                sourceList.RemoveAt(index);

                if (Equals(sourceList, destinationList) && index < insertIndex)
                {
                    --insertIndex;
                }
            }

            return insertIndex;
        }

        protected static bool CanAcceptData(IDropInfo dropInfo)
        {
            if (Equals(dropInfo.DragInfo.SourceCollection, dropInfo.TargetCollection))
            {
                return GetList(dropInfo.TargetCollection) != null;
            }

            if (dropInfo.DragInfo.SourceCollection is ItemCollection)
            {
                return false;
            }

            if (TestCompatibleTypes(dropInfo.TargetCollection, dropInfo.Data))
            {
                return !IsChildOf(dropInfo.VisualTargetItem, dropInfo.DragInfo.VisualSourceItem);
            }

            return false;
        }

        protected static IEnumerable ExtractData(object data)
        {
            var enumerable = data as IEnumerable;
            if (enumerable != null && !(data is string))
            {
                return enumerable;
            }

            return Enumerable.Repeat(data, 1);
        }

        protected static IList GetList(IEnumerable enumerable)
        {
            var collectionView = enumerable as ICollectionView;
            if (collectionView != null)
            {
                return collectionView.SourceCollection as IList;
            }

            return enumerable as IList;
        }

        protected static bool IsChildOf(UIElement targetItem, UIElement sourceItem)
        {
            var parent = ItemsControl.ItemsControlFromItemContainer(targetItem);

            while (parent != null)
            {
                if (Equals(parent, sourceItem))
                {
                    return true;
                }

                parent = ItemsControl.ItemsControlFromItemContainer(parent);
            }

            return false;
        }

        protected static bool TestCompatibleTypes(IEnumerable target, object data)
        {
            TypeFilter filter = (t, o) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);

            var enumerableInterfaces = target.GetType().FindInterfaces(filter, null);
            var enumerableTypes = from i in enumerableInterfaces select i.GetGenericArguments().Single();

            var enumerable = enumerableTypes as Type[] ?? enumerableTypes.ToArray();
            if (!enumerable.Any())
            {
                return target is IList;
            }

            var dataType = TypeUtilities.GetCommonBaseClass(ExtractData(data));
            return enumerable.Any(t => t.IsAssignableFrom(dataType));
        }
    }
}