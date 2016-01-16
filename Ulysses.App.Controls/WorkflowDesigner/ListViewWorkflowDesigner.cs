using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.ProcessingChainDesigner;

namespace Ulysses.App.Controls.WorkflowDesigner
{
    public class ListViewWorkflowDesigner<T> : ListView where T : class
    {
        private bool _canInitiateDrag;
        private DragAdorner _dragAdorner;
        private double _dragAdornerOpacity;
        private Point? _dragStartPosition;
        private int? _indexToSelect;
        private bool _isDragInProgress;
        private T _itemUnderDragCursor;
        private Orientation _orientation;

        public ListViewWorkflowDesigner()
        {
            AllowDrop = true;
            // TODO: Remove and create attached property
            _orientation = Orientation.Horizontal;
            DragAdornerOpacity = 0.7;
            // TODO: END
            PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            PreviewMouseMove += OnPreviewMouseMove;
            DragOver += OnDragOver;
            DragLeave += OnDragLeave;
            DragEnter += OnDragEnter;
            Drop += OnDrop;
        }

        #region Style

        public double DragAdornerOpacity
        {
            get
            {
                return _dragAdornerOpacity;
            }
            set
            {
                if (_isDragInProgress)
                {
                    const string message = "Cannot set the DragAdornerOpacity property during a drag operation.";
                    throw new InvalidOperationException(message);
                }

                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentOutOfRangeException("DragAdornerOpacity", value, "Must be between 0 and 1.");
                }

                _dragAdornerOpacity = value;
            }
        }

        #endregion

        private T ItemUnderDragCursor
        {
            get
            {
                return _itemUnderDragCursor;
            }
            set
            {
                if (_itemUnderDragCursor == value)
                {
                    return;
                }

                SetListViewItemDragStatus(_itemUnderDragCursor, false);

                _itemUnderDragCursor = value;

                SetListViewItemDragStatus(_itemUnderDragCursor, true);
            }
        }

        private void SetListViewItemDragStatus(T item, bool isUnderDragCursor)
        {
            if (item == null)
            {
                return;
            }

            var listViewItem = this.GetListViewItem(item);
            if (listViewItem != null)
            {
                ListViewItemDragState.SetIsUnderDragCursor(listViewItem, isUnderDragCursor);
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);

            if (this.IsMouseOverScrollbar(mousePosition))
            {
                _canInitiateDrag = false;
                return;
            }

            var index = this.GetIndexOfItemUnderCursor(e.GetPosition);
            _canInitiateDrag = index != null;

            if (_canInitiateDrag)
            {
                _dragStartPosition = mousePosition;
                _indexToSelect = index;
                return;
            }

            _dragStartPosition = null;
            _indexToSelect = null;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!CanStartDragOperation(e.GetPosition(this)))
            {
                return;
            }

            if (!_indexToSelect.HasValue)
            {
                return;
            }

            if (SelectedIndex != _indexToSelect.Value)
            {
                SelectedIndex = _indexToSelect.Value;
            }

            if (SelectedItem == null)
            {
                return;
            }

            var itemToDrag = this.GetListViewItem(SelectedIndex);
            if (itemToDrag == null)
            {
                return;
            }

            var adornerLayer = InitializeAdornerLayer(itemToDrag);

            _dragStartPosition = e.GetPosition(this);
            InitializeDragOperation(itemToDrag);
            PerformDragOperation();
            FinishDragOperation(itemToDrag, adornerLayer);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            UpdateDragAdornerLocation(e.GetPosition(this));
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            if (this.IsMouseOverVisual(e.GetPosition(this)))
            {
                return;
            }

            if (ItemUnderDragCursor != null)
            {
                ItemUnderDragCursor = null;
            }

            if (_dragAdorner != null)
            {
                _dragAdorner.Visibility = Visibility.Collapsed;
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (_dragAdorner == null || _dragAdorner.Visibility == Visibility.Visible)
            {
                return;
            }

            UpdateDragAdornerLocation(e.GetPosition(this));
            _dragAdorner.Visibility = Visibility.Visible;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (ItemUnderDragCursor != null)
            {
                ItemUnderDragCursor = null;
            }

            e.Effects = DragDropEffects.None;

            if (!e.Data.GetDataPresent(typeof (T)))
            {
                return;
            }

            var data = e.Data.GetData(typeof (T)) as T;
            if (data == null)
            {
                return;
            }

            var itemsSource = ItemsSource as ObservableCollection<T>;
            if (itemsSource == null)
            {
                throw new Exception("A ListView managed by ListViewDragManager must have its ItemsSource set to an ObservableCollection<ItemType>.");
            }


            int? oldIndex = itemsSource.IndexOf(data);
            if (oldIndex.Value < 0)
            {
                oldIndex = null;
            }

            var newIndex = this.GetIndexOfItemUnderCursor(e.GetPosition);
            if (oldIndex == newIndex)
            {
                return;
            }

            PlaceItemInCollection(itemsSource, newIndex, oldIndex, data);
            e.Effects = DragDropEffects.Move;
        }

        private static void PlaceItemInCollection(ObservableCollection<T> itemsSource, int? newIndex, int? oldIndex, T item)
        {
            if (!newIndex.HasValue && itemsSource.Count == 0)
            {
                newIndex = 0;
            }
            else if (!newIndex.HasValue && !oldIndex.HasValue)
            {
                newIndex = itemsSource.Count;
            }
            else if (!newIndex.HasValue)
            {
                return;
            }

            if (oldIndex.HasValue)
            {
                itemsSource.Move(oldIndex.Value, newIndex.Value);
                return;
            }

            itemsSource.Insert(newIndex.Value, item);
        }

        private bool CanStartDragOperation(Point currentMousePosition)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                return false;
            }

            if (!_canInitiateDrag)
            {
                return false;
            }

            return _indexToSelect.HasValue && HasCursorLeftDragThreshold(currentMousePosition);
        }

        private void FinishDragOperation(ListViewItem draggedItem, AdornerLayer adornerLayer)
        {
            ListViewItemDragState.SetIsBeingDragged(draggedItem, false);

            _isDragInProgress = false;

            if (ItemUnderDragCursor != null)
            {
                ItemUnderDragCursor = null;
            }

            if (adornerLayer == null)
            {
                return;
            }

            adornerLayer.Remove(_dragAdorner);
            _dragAdorner = null;
        }

        private bool HasCursorLeftDragThreshold(Point currentMousePosition)
        {
            if (!_indexToSelect.HasValue)
            {
                return false;
            }

            var item = this.GetListViewItem(_indexToSelect.Value);
            var bounds = VisualTreeHelper.GetDescendantBounds(item);

            if (!_dragStartPosition.HasValue)
            {
                return false;
            }

            // TODO:REFACTOR FROM HERE
            var ptInItem = TranslatePoint(_dragStartPosition.Value, item);

            var topOffset = Math.Abs(ptInItem.Y);
            var bottomOffset = Math.Abs(bounds.Height - ptInItem.Y);
            var verticalOffset = Math.Min(topOffset, bottomOffset);

            var width = SystemParameters.MinimumHorizontalDragDistance * 2;
            var height = Math.Min(SystemParameters.MinimumVerticalDragDistance, verticalOffset) * 2;
            var thresholdSize = new Size(width, height);

            var dragThresholdRectangle = new Rect(_dragStartPosition.Value, thresholdSize);

            dragThresholdRectangle.Offset(thresholdSize.Width / -2, thresholdSize.Height / -2);

            return !dragThresholdRectangle.Contains(currentMousePosition);
        }

        private AdornerLayer InitializeAdornerLayer(UIElement itemToDrag)
        {
            var brush = new VisualBrush(itemToDrag);

            _dragAdorner = new DragAdorner(this, itemToDrag.RenderSize, brush) { Opacity = DragAdornerOpacity };

            var layer = AdornerLayer.GetAdornerLayer(this);
            layer.Add(_dragAdorner);

            // TODO: WAT?
            //_dragStartPosition = Mouse.GetPosition(this);

            return layer;
        }

        private void InitializeDragOperation(ListViewItem itemToDrag)
        {
            _isDragInProgress = true;
            _canInitiateDrag = false;

            ListViewItemDragState.SetIsBeingDragged(itemToDrag, true);
        }

        private void PerformDragOperation()
        {
            const DragDropEffects allowedEffects = DragDropEffects.Move | DragDropEffects.Move | DragDropEffects.Link;

            var selectedItem = SelectedItem;

            if (DragDrop.DoDragDrop(this, selectedItem, allowedEffects) != DragDropEffects.None)
            {
                SelectedItem = selectedItem;
            }
        }

        private void UpdateDragAdornerLocation(Point mousePosition)
        {
            if (_dragAdorner == null)
            {
                return;
            }

            if (!_indexToSelect.HasValue)
            {
                return;
            }

            if (!_dragStartPosition.HasValue)
            {
                return;
            }

            var itemBeingDragged = this.GetListViewItem(_indexToSelect.Value);
            var itemLocation = itemBeingDragged.TranslatePoint(new Point(0, 0), this);

            double left;
            double top;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    top = mousePosition.Y - _dragStartPosition.Value.Y;
                    left = itemLocation.X + mousePosition.X - _dragStartPosition.Value.X;
                    break;
                case Orientation.Vertical:
                    left = mousePosition.X - _dragStartPosition.Value.X;
                    top = itemLocation.Y + mousePosition.Y - _dragStartPosition.Value.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _dragAdorner.SetTransformOffsets(left, top);
        }
    }
}