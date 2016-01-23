using System.Collections;
using System.Windows;
using System.Windows.Input;

namespace Ulysses.App.Controls.DragAndDropExtension
{
    public interface IDragInfo
    {
        object Data { get; set; }
        Point DragStartPosition { get; }
        DragDropEffects Effects { get; set; }
        MouseButton MouseButton { get; }
        IEnumerable SourceCollection { get; }
        object SourceItem { get; }
        IEnumerable SourceItems { get; set; }
        UIElement VisualSource { get; }
        UIElement VisualSourceItem { get; }
    }
}