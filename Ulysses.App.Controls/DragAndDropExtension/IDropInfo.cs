using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Ulysses.App.Controls.DragAndDropExtension
{
    public interface IDropInfo
    {
        object Data { get; }
        IDragInfo DragInfo { get; }
        Type DropTargetAdorner { get; set; }
        DragDropEffects Effects { get; set; }
        int InsertIndex { get; }
        IEnumerable TargetCollection { get; }
        object TargetItem { get; }
        UIElement VisualTarget { get; }
        UIElement VisualTargetItem { get; }
        Orientation VisualTargetOrientation { get; }
    }
}