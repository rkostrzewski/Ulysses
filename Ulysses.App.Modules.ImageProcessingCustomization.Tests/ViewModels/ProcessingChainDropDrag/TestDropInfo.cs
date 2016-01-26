using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Ulysses.App.Controls.DragAndDropExtension;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ProcessingChainDropDrag
{
    public class TestDropInfo : IDropInfo
    {
        public object Data { get; set; }
        public IDragInfo DragInfo { get; set; }
        public Type DropTargetAdorner { get; set; }
        public DragDropEffects Effects { get; set; }
        public int InsertIndex { get; set; }
        public IEnumerable TargetCollection { get; set; }
        public object TargetItem { get; }
        public UIElement VisualTarget { get; }
        public UIElement VisualTargetItem { get; }
        public Orientation VisualTargetOrientation { get; }
    }
}