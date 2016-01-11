using System.Windows;
using System.Windows.Controls;
using Ulysses.App.Controls.Workflows.DesignerCanvas;

namespace Ulysses.App.Controls.Workflows.Toolbox
{
    public class WorkflowToolbox : ItemsControl
    {
        public WorkflowToolbox()
        {
            DefaultItemSize = DesignerConfiguration.DefaultItemSize;
        }

        public Size DefaultItemSize { get; set; }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new WorkflowToolboxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is WorkflowToolboxItem;
        }
    }
}
