using System.Windows;
using System.Windows.Controls;

namespace Ulysses.App.Controls.WorkFlows
{
    public class WorkFlowDesigner : Control
    {
        static WorkFlowDesigner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkFlowDesigner), new FrameworkPropertyMetadata(typeof(WorkFlowDesigner)));
        }
    }
}
