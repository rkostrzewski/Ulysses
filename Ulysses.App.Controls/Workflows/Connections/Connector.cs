using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Annotations;
using Ulysses.App.Controls.Workflows.DesignerCanvas;
using Ulysses.App.Controls.Workflows.Items;

namespace Ulysses.App.Controls.Workflows.Connections
{
    public class Connector : Control, INotifyPropertyChanged
    {
        private Point? _dragStartPoint;
        private WorkflowItem _parentItem;
        private Point _position;

        public Connector()
        {
            Connections = new List<Connection>();
            LayoutUpdated += OnLayoutUpdated;
        }

        public WorkflowItem ParentItem
        {
            get
            {
                if (_parentItem != null)
                {
                    _parentItem = DataContext as WorkflowItem;
                }

                return _parentItem;
            }
        }

        public ConnectorOrientation Orientation { get; set; }

        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position == value)
                {
                    return;
                }

                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public IList<Connection> Connections { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var canvas = GetWorkflowDesignerCanvas(this);

            if (canvas == null)
            {
                return;
            }

            _dragStartPoint = e.GetPosition(canvas);
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

            var canvas = GetWorkflowDesignerCanvas(this);
            if (canvas == null)
            {
                return;
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
            if (adornerLayer == null)
            {
                return;
            }

            var adorner = new ConnectorAdorner(canvas, this);
            adornerLayer.Add(adorner);
            e.Handled = true;
        }

        internal ConnectorInfo GetInfo()
        {
            return new ConnectorInfo
            {
                ItemLeft = Canvas.GetLeft(ParentItem),
                ItemTop = Canvas.GetTop(ParentItem),
                ItemSize = new Size(ParentItem.ActualWidth, ParentItem.ActualHeight),
                Orientation = Orientation,
                Position = Position
            };
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            var canvas = GetWorkflowDesignerCanvas(this);
            if (canvas == null)
            {
                return;
            }

            var pointToTransformTo = new Point(Width / 2, Height / 2);
            Position = TransformToAncestor(canvas).Transform(pointToTransformTo);
        }

        private static WorkflowDesignerCanvas GetWorkflowDesignerCanvas(DependencyObject element)
        {
            var searchedElement = element;
            while (searchedElement != null && !(searchedElement is WorkflowDesignerCanvas))
            {
                searchedElement = VisualTreeHelper.GetParent(searchedElement);
            }

            return searchedElement as WorkflowDesignerCanvas;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}