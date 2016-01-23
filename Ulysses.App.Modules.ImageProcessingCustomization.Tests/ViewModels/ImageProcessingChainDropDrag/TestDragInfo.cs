using System.Collections;
using System.Windows;
using System.Windows.Input;
using Ulysses.App.Controls.DragAndDropExtension;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ImageProcessingChainDropDrag
{
    public class TestDragInfo : IDragInfo
    {
        public object Data { get; set; }
        public Point DragStartPosition { get; }
        public DragDropEffects Effects { get; set; }
        public MouseButton MouseButton { get; }
        public IEnumerable SourceCollection { get; set; }
        public object SourceItem { get; }
        public IEnumerable SourceItems { get; set; }
        public UIElement VisualSource { get; }
        public UIElement VisualSourceItem { get; }
    }
}