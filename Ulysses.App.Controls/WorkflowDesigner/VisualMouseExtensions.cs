using System.Windows;
using System.Windows.Media;

namespace Ulysses.App.Controls.WorkflowDesigner
{
    public static class VisualMouseExtensions
    {
        public static bool IsMouseOverVisual(this Visual target, Point mousePosition)
        {
            var targetBounds = VisualTreeHelper.GetDescendantBounds(target);
            
            return targetBounds.Contains(mousePosition);
        }
    }
}