using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.DesignerCanvas;
using Ulysses.App.Controls.Workflows.Items;
using Ulysses.App.Controls.Workflows.Templates;

namespace Ulysses.App.Controls.Workflows.Connections
{
    public class ConnectionAdorner : Adorner
    {
        private readonly Canvas _adornerCanvas;
        private readonly Connection _connection;
        private readonly WorkflowDesignerCanvas _designerCanvas;
        private readonly VisualCollection _visualChildren;
        private Connector _dragConnector;
        private Connector _fixConnector;
        private Connector _hitConnector;
        private WorkflowItem _hitDesignerItem;
        private Geometry _pathGeometry;
        private Thumb _sinkDragThumb;
        private Thumb _sourceDragThumb;

        public ConnectionAdorner(WorkflowDesignerCanvas canvas, Connection connection) : base(canvas)
        {
            _designerCanvas = canvas;
            _connection = connection;
            _connection.PropertyChanged += OnAnchorPositionChanged;
            _adornerCanvas = new Canvas();
            _visualChildren = new VisualCollection(this) { _adornerCanvas };

            InitializeDragThumbs();
        }

        protected override int VisualChildrenCount => _visualChildren.Count;

        private void InitializeDragThumbs()
        {
            var dragThumbStyle = _connection.FindResource(StyleNames.ConnectionAdornerThumbStyle) as Style;

            _sourceDragThumb = new Thumb();
            Canvas.SetLeft(_sourceDragThumb, _connection.AnchorPositionSource.X);
            Canvas.SetTop(_sourceDragThumb, _connection.AnchorPositionSource.Y);
            _adornerCanvas.Children.Add(_sourceDragThumb);
            if (dragThumbStyle != null)
            {
                _sourceDragThumb.Style = dragThumbStyle;
            }

            _sourceDragThumb.DragDelta += thumbDragThumb_DragDelta;
            _sourceDragThumb.DragStarted += thumbDragThumb_DragStarted;
            _sourceDragThumb.DragCompleted += thumbDragThumb_DragCompleted;

            _sinkDragThumb = new Thumb();
            Canvas.SetLeft(_sinkDragThumb, _connection.AnchorPositionSink.X);
            Canvas.SetTop(_sinkDragThumb, _connection.AnchorPositionSink.Y);
            _adornerCanvas.Children.Add(_sinkDragThumb);
            if (dragThumbStyle != null)
            {
                _sinkDragThumb.Style = dragThumbStyle;
            }

            _sinkDragThumb.DragDelta += thumbDragThumb_DragDelta;
            _sinkDragThumb.DragStarted += thumbDragThumb_DragStarted;
            _sinkDragThumb.DragCompleted += thumbDragThumb_DragCompleted;
        }

        protected override Visual GetVisualChild(int index) => _visualChildren[index];

        private void OnAnchorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Connection.AnchorPositionSource):
                    Canvas.SetLeft(_sourceDragThumb, _connection.AnchorPositionSource.X);
                    Canvas.SetTop(_sourceDragThumb, _connection.AnchorPositionSource.Y);
                    return;
                case nameof(Connection.AnchorPositionSink):
                    Canvas.SetLeft(_sinkDragThumb, _connection.AnchorPositionSink.X);
                    Canvas.SetTop(_sinkDragThumb, _connection.AnchorPositionSink.Y);
                    return;
                default:
                    return;
            }
        }

        private void thumbDragThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (_hitConnector != null)
            {
                if (Equals(_connection.Source, _fixConnector))
                {
                    _connection.Sink = _hitConnector;
                }
                else
                {
                    _connection.Source = _hitConnector;
                }
            }

            _hitDesignerItem = null;
            _hitConnector = null;
            _pathGeometry = null;
            _connection.StrokeDashArray = null;
            InvalidateVisual();
        }

        private void thumbDragThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _hitDesignerItem = null;
            _hitConnector = null;
            _pathGeometry = null;
            _connection.StrokeDashArray = new DoubleCollection(new double[] { 1, 2 });
            Cursor = Cursors.Cross;

            if (Equals(sender, _sourceDragThumb))
            {
                _fixConnector = _connection.Sink;
                _dragConnector = _connection.Source;
            }
            else if (Equals(sender, _sinkDragThumb))
            {
                _dragConnector = _connection.Sink;
                _fixConnector = _connection.Source;
            }
        }

        private void thumbDragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var currentPosition = Mouse.GetPosition(this);
            HitTesting(currentPosition);
            _pathGeometry = UpdatePathGeometry(currentPosition);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var drawingPen = new Pen(Brushes.LightSlateGray, 1) { LineJoin = PenLineJoin.Round };
            dc.DrawGeometry(null, drawingPen, _pathGeometry);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _designerCanvas.Arrange(new Rect(0, 0, _designerCanvas.ActualWidth, _designerCanvas.ActualHeight));
            return finalSize;
        }

        private PathGeometry UpdatePathGeometry(Point position)
        {
            var geometry = new PathGeometry();

            var targetOrientation = _hitConnector?.Orientation ?? _dragConnector.Orientation;

            var linePoints = PathFinder.GetConnectionLine(_fixConnector.GetInfo(), position, targetOrientation);

            if (linePoints.Count <= 0)
            {
                return geometry;
            }

            var figure = new PathFigure { StartPoint = linePoints[0] };
            linePoints.Remove(linePoints[0]);
            figure.Segments.Add(new PolyLineSegment(linePoints, true));
            geometry.Figures.Add(figure);

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            var hitConnectorFlag = false;

            var hitObject = _designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null && hitObject != _fixConnector.ParentItem && hitObject.GetType() != typeof (WorkflowDesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    _hitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is WorkflowItem)
                {
                    if (_hitDesignerItem != null)
                    {
                        _hitDesignerItem.IsDragConnectionOver = false;
                    }

                    _hitDesignerItem = hitObject as WorkflowItem;

                    if (_hitDesignerItem != null)
                    {
                        _hitDesignerItem.IsDragConnectionOver = true;
                    }

                    if (!hitConnectorFlag)
                    {
                        _hitConnector = null;
                    }
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            _hitConnector = null;
            _hitDesignerItem = null;
        }
    }
}