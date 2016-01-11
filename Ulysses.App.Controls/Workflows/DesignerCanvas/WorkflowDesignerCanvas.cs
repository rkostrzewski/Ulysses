using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using Ulysses.App.Controls.Workflows.Adorners;
using Ulysses.App.Controls.Workflows.Items;

namespace Ulysses.App.Controls.Workflows.DesignerCanvas
{
    public class WorkflowDesignerCanvas : Canvas
    {
        private Point? _dragStartPoint;

        public IList<ISelectable> SelectedItems { get; }

        public void DeselectAll()
        {
            foreach (var item in SelectedItems)
            {
                item.IsSelected = false;
            }
        }

        public WorkflowDesignerCanvas()
        {
            SelectedItems = new List<ISelectable>();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var size = new Size();

            foreach (UIElement element in Children)
            {
                var left = double.IsNaN(GetLeft(element)) ? 0 : GetLeft(element);
                var top = double.IsNaN(GetTop(element)) ? 0 : GetTop(element);

                element.Measure(constraint);

                var desiredSize = element.DesiredSize;

                if (double.IsNaN(desiredSize.Width) || double.IsNaN(desiredSize.Height))
                {
                    continue;
                }

                size.Width = Math.Max(size.Width, left + desiredSize.Width);
                size.Height = Math.Max(size.Height, top + desiredSize.Height);
            }

            size.Width += DesignerConfiguration.CanvasPadding.Width;
            size.Height += DesignerConfiguration.CanvasPadding.Height;

            return size;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (!Equals(e.Source, this))
            {
                return;
            }

            _dragStartPoint = e.GetPosition(this);
            DeselectAll();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _dragStartPoint = null;
            }

            if (!_dragStartPoint.HasValue)
            {
                return;
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(this);

            if (adornerLayer != null)
            {
                var adorner = new RubberBandAdorner(this, _dragStartPoint.Value);
                adornerLayer.Add(adorner);
            }

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            var xamlString = e.Data.GetData(nameof(WorkflowItem)) as string;

            if (string.IsNullOrEmpty(xamlString))
            {
                return;
            }

            e.Handled = true;

            var content = XamlReader.Load(XmlReader.Create(new StringReader(xamlString))) as FrameworkElement;

            if (content == null)
            {
                return;
            }

            var newItem = new WorkflowItem { Content = content };
            var position = e.GetPosition(this);

            if (Math.Abs(content.MinHeight) > double.Epsilon && Math.Abs(content.MinWidth) > double.Epsilon)
            {
                newItem.Width = content.MinWidth * 2;
                newItem.Height = content.MinHeight * 2;
            }
            else
            {
                newItem.Width = DesignerConfiguration.DefaultItemSize.Width;
                newItem.Height = DesignerConfiguration.DefaultItemSize.Height;
            }

            var left = Math.Max(0, position.X - newItem.Width / 2);
            var top = Math.Max(0, position.Y - newItem.Height / 2);

            SetLeft(newItem, left);
            SetTop(newItem, top);

            Children.Add(newItem);

            DeselectAll();
            newItem.IsSelected = true;
        }
    }
}