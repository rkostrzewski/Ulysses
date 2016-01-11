using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Annotations;
using Ulysses.App.Controls.Workflows.DesignerCanvas;

namespace Ulysses.App.Controls.Workflows.Connections
{
    public class Connection : Control, ISelectable, INotifyPropertyChanged
    {
        private double _anchorAngleSink;
        private double _anchorAngleSource;
        private Point _anchorPositionSink;
        private Point _anchorPositionSource;
        private ConnectionAdorner _connectionAdorner;
        private bool _isSelected;
        private Point _labelPosition;
        private PathGeometry _pathGeometry;
        private Connector _sink;

        private ArrowSymbol _sinkArrowSymbol = ArrowSymbol.Arrow;
        private Connector _source;

        private ArrowSymbol _sourceArrowSymbol = ArrowSymbol.None;

        private DoubleCollection _strokeDashArray;

        public Connection(Connector source, Connector sink)
        {
            Source = source;
            Sink = sink;
            Unloaded += OnUnloaded;
        }

        public Connector Source
        {
            get
            {
                return _source;
            }
            set
            {
                SetConnector(ref _source, value);
            }
        }

        public Connector Sink
        {
            get
            {
                return _sink;
            }
            set
            {
                SetConnector(ref _sink, value);
            }
        }

        public PathGeometry PathGeometry
        {
            get
            {
                return _pathGeometry;
            }
            set
            {
                if (Equals(_pathGeometry, value))
                {
                    return;
                }

                _pathGeometry = value;
                UpdateAnchorPosition();
                OnPropertyChanged(nameof(PathGeometry));
            }
        }

        public Point AnchorPositionSource
        {
            get
            {
                return _anchorPositionSource;
            }
            set
            {
                _anchorPositionSource = value;
                OnPropertyChanged(nameof(AnchorPositionSource));
            }
        }

        public double AnchorAngleSource
        {
            get
            {
                return _anchorAngleSource;
            }
            set
            {
                _anchorAngleSource = value;
                OnPropertyChanged(nameof(AnchorAngleSource));
            }
        }

        public ArrowSymbol SourceArrowSymbol
        {
            get
            {
                return _sourceArrowSymbol;
            }
            set
            {
                if (_sourceArrowSymbol == value)
                {
                    return;
                }
                _sourceArrowSymbol = value;
                OnPropertyChanged(nameof(SourceArrowSymbol));
            }
        }

        public ArrowSymbol SinkArrowSymbol
        {
            get
            {
                return _sinkArrowSymbol;
            }
            set
            {
                if (_sinkArrowSymbol == value)
                {
                    return;
                }
                _sinkArrowSymbol = value;
                OnPropertyChanged(nameof(SinkArrowSymbol));
            }
        }

        public Point LabelPosition
        {
            get
            {
                return _labelPosition;
            }
            set
            {
                _labelPosition = value;
                OnPropertyChanged(nameof(LabelPosition));
            }
        }

        public Point AnchorPositionSink
        {
            get
            {
                return _anchorPositionSink;
            }
            set
            {
                _anchorPositionSink = value;
                OnPropertyChanged(nameof(AnchorPositionSink));
            }
        }

        public double AnchorAngleSink
        {
            get
            {
                return _anchorAngleSink;
            }
            set
            {
                _anchorAngleSink = value;
                OnPropertyChanged(nameof(AnchorAngleSink));
            }
        }

        public DoubleCollection StrokeDashArray
        {
            get
            {
                return _strokeDashArray;
            }
            set
            {
                if (Equals(_strokeDashArray, value))
                {
                    return;
                }

                _strokeDashArray = value;
                OnPropertyChanged(nameof(StrokeDashArray));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                if (_isSelected)
                {
                    ShowAdorner();
                    return;
                }

                HideAdorner();
            }
        }

        private void HideAdorner()
        {
            if (_connectionAdorner != null)
            {
                _connectionAdorner.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowAdorner()
        {
            if (_connectionAdorner == null)
            {
                var canvas = VisualTreeHelper.GetParent(this) as WorkflowDesignerCanvas;

                if (canvas == null)
                {
                    return;
                }

                var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                if (adornerLayer == null)
                {
                    return;
                }

                _connectionAdorner = new ConnectionAdorner(canvas, this);
                adornerLayer.Add(_connectionAdorner);
            }

            _connectionAdorner.Visibility = Visibility.Visible;
        }

        private void SetConnector(ref Connector oldValue, Connector newValue)
        {
            if (Equals(oldValue, newValue))
            {
                return;
            }

            if (oldValue != null)
            {
                oldValue.PropertyChanged -= OnConnectorPositionChanged;
                oldValue.Connections.Remove(this);
            }

            oldValue = newValue;

            if (oldValue != null)
            {
                oldValue.PropertyChanged += OnConnectorPositionChanged;
                oldValue.Connections.Add(this);
            }

            UpdatePathGeometry();
        }

        private void OnConnectorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Connector.Position)))
            {
                UpdatePathGeometry();
            }
        }

        private void UpdatePathGeometry()
        {
            if (Source == null || Sink == null)
            {
                return;
            }

            var geometry = new PathGeometry();
            var pathPoints = PathFinder.GetConnectionLine(Source.GetInfo(), Sink.GetInfo(), true);

            if (pathPoints.Count <= 0)
            {
                return;
            }

            var firstPoint = pathPoints.First();
            pathPoints.RemoveAt(0);

            var figure = new PathFigure { StartPoint = firstPoint };
            figure.Segments.Add(new PolyLineSegment(pathPoints, true));
            geometry.Figures.Add(figure);

            PathGeometry = geometry;
        }

        private void UpdateAnchorPosition()
        {
            Point pathStartPoint, pathTangentAtStartPoint;
            Point pathEndPoint, pathTangentAtEndPoint;
            Point pathMidPoint, pathTangentAtMidPoint;

            PathGeometry.GetPointAtFractionLength(0, out pathStartPoint, out pathTangentAtStartPoint);
            PathGeometry.GetPointAtFractionLength(0.5, out pathMidPoint, out pathTangentAtMidPoint);
            PathGeometry.GetPointAtFractionLength(1, out pathEndPoint, out pathTangentAtEndPoint);

            AnchorAngleSource = Math.Atan2(-pathTangentAtStartPoint.Y, -pathTangentAtStartPoint.X) * (180 / Math.PI);
            AnchorAngleSink = Math.Atan2(pathTangentAtEndPoint.Y, pathTangentAtEndPoint.X) * (180 / Math.PI);

            var margin = 5;
            pathStartPoint.Offset(-pathTangentAtStartPoint.X * margin, -pathTangentAtStartPoint.Y * margin);
            pathEndPoint.Offset(pathTangentAtEndPoint.X * margin, pathTangentAtEndPoint.Y * margin);

            AnchorPositionSource = pathStartPoint;
            AnchorPositionSink = pathEndPoint;
            LabelPosition = pathMidPoint;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            e.Handled = false;

            var canvas = VisualTreeHelper.GetParent(this) as WorkflowDesignerCanvas;
            if (canvas == null)
            {
                return;
            }

            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
            {
                if (IsSelected)
                {
                    IsSelected = false;
                    canvas.SelectedItems.Remove(this);
                    return;
                }

                IsSelected = true;
                canvas.SelectedItems.Add(this);
                return;
            }

            if (IsSelected)
            {
                return;
            }

            foreach (var item in canvas.SelectedItems)
            {
                item.IsSelected = false;
            }

            IsSelected = true;
            canvas.SelectedItems.Clear();
            canvas.SelectedItems.Add(this);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _source.PropertyChanged -= OnConnectorPositionChanged;
            _sink.PropertyChanged -= OnConnectorPositionChanged;

            if (_connectionAdorner == null)
            {
                return;
            }

            var canvas = VisualTreeHelper.GetParent(this) as WorkflowDesignerCanvas;
            if (canvas != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                adornerLayer?.Remove(_connectionAdorner);
            }

            _connectionAdorner = null;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}