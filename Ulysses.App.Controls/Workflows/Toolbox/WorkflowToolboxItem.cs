using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Ulysses.App.Controls.Workflows.Items;

namespace Ulysses.App.Controls.Workflows.Toolbox
{
    public class WorkflowToolboxItem : ContentControl
    {
        private Point? _dragStartPoint;

        static WorkflowToolboxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WorkflowToolboxItem), new FrameworkPropertyMetadata(typeof (WorkflowToolboxItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            _dragStartPoint = e.GetPosition(this);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            if (!_dragStartPoint.HasValue)
            {
                return;
            }

            var currentPosition = e.GetPosition(this);
            if ((SystemParameters.MinimumHorizontalDragDistance <= Math.Abs(currentPosition.X - _dragStartPoint.Value.X)) ||
                (SystemParameters.MinimumVerticalDragDistance <= Math.Abs(currentPosition.Y - _dragStartPoint.Value.Y)))
            {
                var xamlString = XamlWriter.Save(Content);
                var dataObject = new DataObject(nameof(WorkflowItem), xamlString);

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
            }

            e.Handled = true;
        }
    }
}