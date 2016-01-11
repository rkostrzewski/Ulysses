using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.DesignerCanvas;
using Ulysses.App.Controls.Workflows.Items;

namespace Ulysses.App.Controls.Workflows.Connections
{
    public class ConnectorAdorner : Adorner
    {
        private readonly WorkflowDesignerCanvas _designerCanvas;
        private readonly Pen _drawingPen;
        private readonly Connector _sourceConnector;
        private Connector _hitConnector;
        private WorkflowItem _hitDesignerItem;
        private PathGeometry _pathGeometry;

        public ConnectorAdorner(WorkflowDesignerCanvas designer, Connector sourceConnector) : base(designer)
        {
            _designerCanvas = designer;
            _sourceConnector = sourceConnector;
            _drawingPen = new Pen(Brushes.LightSlateGray, 1) { LineJoin = PenLineJoin.Round };
            Cursor = Cursors.Cross;
        }

        private WorkflowItem HitDesignerItem
        {
            get
            {
                return _hitDesignerItem;
            }
            set
            {
                if (Equals(_hitDesignerItem, value))
                {
                    return;
                }
                if (_hitDesignerItem != null)
                {
                    _hitDesignerItem.IsDragConnectionOver = false;
                }

                _hitDesignerItem = value;

                if (_hitDesignerItem != null)
                {
                    _hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private Connector HitConnector
        {
            get
            {
                return _hitConnector;
            }
            set
            {
                if (!Equals(_hitConnector, value))
                {
                    _hitConnector = value;
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (HitConnector != null)
            {
                var sourceConnector = _sourceConnector;
                var sinkConnector = HitConnector;
                var newConnection = new Connection(sourceConnector, sinkConnector);

                _designerCanvas.Children.Insert(0, newConnection);
            }
            if (HitDesignerItem != null)
            {
                HitDesignerItem.IsDragConnectionOver = false;
            }

            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(_designerCanvas);
            adornerLayer?.Remove(this);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!IsMouseCaptured)
                {
                    CaptureMouse();
                }
                HitTesting(e.GetPosition(this));
                _pathGeometry = GetPathGeometry(e.GetPosition(this));
                InvalidateVisual();
            }
            else
            {
                if (IsMouseCaptured)
                {
                    ReleaseMouseCapture();
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, _drawingPen, _pathGeometry);
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            var geometry = new PathGeometry();

            var targetOrientation = HitConnector?.Orientation ?? ConnectorOrientation.None;

            var pathPoints = PathFinder.GetConnectionLine(_sourceConnector.GetInfo(), position, targetOrientation);

            if (pathPoints.Count <= 0)
            {
                return geometry;
            }

            var figure = new PathFigure { StartPoint = pathPoints[0] };
            pathPoints.Remove(pathPoints[0]);
            figure.Segments.Add(new PolyLineSegment(pathPoints, true));
            geometry.Figures.Add(figure);

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            var hitConnectorFlag = false;

            var hitObject = _designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null && !Equals(hitObject, _sourceConnector.ParentItem) && hitObject.GetType() != typeof (WorkflowDesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is WorkflowItem)
                {
                    HitDesignerItem = hitObject as WorkflowItem;
                    if (!hitConnectorFlag)
                    {
                        HitConnector = null;
                    }
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HitConnector = null;
            HitDesignerItem = null;
        }
    }
}