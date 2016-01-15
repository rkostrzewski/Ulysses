using System.Windows;

namespace Ulysses.App.Controls.WorkflowDesigner
{
    public interface IMousePositionRelatedEvent
    {
        Point GetPosition(IInputElement relativeTo);
    }
}