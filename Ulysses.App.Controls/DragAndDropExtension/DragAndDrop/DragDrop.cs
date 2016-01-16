using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.DragAndDropExtension.Adorners;
using Ulysses.App.Controls.DragAndDropExtension.Extensions;
using Ulysses.App.Controls.DragAndDropExtension.Handlers;

namespace Ulysses.App.Controls.DragAndDropExtension.DragAndDrop
{
    public static partial class DragDrop
    {
        private static IDragSource _defaultDragHandler;
        private static IDropTarget _defaultDropHandler;
        private static DragAdorner _dragAdorner;
        private static DragInfo _dragInfo;
        private static DropTargetAdorner _dropTargetAdorner;
        private static readonly DataFormat Format = DataFormats.GetDataFormat(typeof (DragDrop).FullName);

        private static IDragSource DefaultDragHandler
        {
            get
            {
                return _defaultDragHandler ?? (_defaultDragHandler = new DefaultDragHandler());
            }
            set
            {
                _defaultDragHandler = value;
            }
        }

        private static IDropTarget DefaultDropHandler
        {
            get
            {
                return _defaultDropHandler ?? (_defaultDropHandler = new DefaultDropHandler());
            }
            set
            {
                _defaultDropHandler = value;
            }
        }

        private static DragAdorner DragAdorner
        {
            get
            {
                return _dragAdorner;
            }
            set
            {
                _dragAdorner?.Detach();

                _dragAdorner = value;
            }
        }

        private static DropTargetAdorner DropTargetAdorner
        {
            get
            {
                return _dropTargetAdorner;
            }
            set
            {
                _dropTargetAdorner?.Detach();

                _dropTargetAdorner = value;
            }
        }

        public static void OnIsDragSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;

            if ((bool)e.NewValue)
            {
                uiElement.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
                uiElement.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
                uiElement.PreviewMouseMove += OnPreviewMouseMove;
                return;
            }

            uiElement.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            uiElement.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
            uiElement.PreviewMouseMove -= OnPreviewMouseMove;
        }

        public static void OnIsDropTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;

            if ((bool)e.NewValue)
            {
                uiElement.AllowDrop = true;
                uiElement.PreviewDragEnter += OnPreviewDragEnter;
                uiElement.PreviewDragLeave += OnPreviewDragLeave;
                uiElement.PreviewDragOver += OnPreviewDragOver;
                uiElement.PreviewDrop += OnPreviewDrop;
                return;
            }

            uiElement.AllowDrop = false;
            uiElement.PreviewDragEnter -= OnPreviewDragEnter;
            uiElement.PreviewDragLeave -= OnPreviewDragLeave;
            uiElement.PreviewDragOver -= OnPreviewDragOver;
            uiElement.PreviewDrop -= OnPreviewDrop;
        }

        private static void CreateDragAdorner(UIElement dragSource)
        {
            var template = GetDragAdornerTemplate(_dragInfo.VisualSource);

            if (template == null)
            {
                return;
            }

            var rootElement = (UIElement)Application.Current.MainWindow.Content;
            UIElement adornment;

            var data = _dragInfo.Data as IEnumerable;
            if (data != null && !(_dragInfo.Data is string))
            {
                // TODO: WAAAAAAAAAT
                //if (data.Cast<object>().Count() <= 10)
                //{  
                //}

                var border = new Border { Child = new ItemsControl { ItemsSource = data, ItemTemplate = template } };
                adornment = border;
            }
            else
            {
                var contentPresenter = new ContentPresenter { Content = _dragInfo.Data, ContentTemplate = template };
                adornment = contentPresenter;
            }

            const int defaultAdornerOpacity = 1;
            adornment.Opacity = defaultAdornerOpacity;

            if (dragSource != null)
            {
                adornment.Opacity = GetAdornmentOpacity(dragSource);
            }

            DragAdorner = new DragAdorner(rootElement, adornment);
        }

        private static bool HitTestScrollBar(object sender, MouseEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            return hit.VisualHit.GetVisualAncestor<ScrollBar>() != null;
        }

        private static void Scroll(DependencyObject o, DragEventArgs e)
        {
            var scrollViewer = o.GetVisualDescendent<ScrollViewer>();

            if (scrollViewer == null)
            {
                return;
            }

            var position = e.GetPosition(scrollViewer);
            var scrollMargin = Math.Min(scrollViewer.FontSize * 2, scrollViewer.ActualHeight / 2);

            if (position.X >= scrollViewer.ActualWidth - scrollMargin && scrollViewer.HorizontalOffset < scrollViewer.ExtentWidth - scrollViewer.ViewportWidth)
            {
                scrollViewer.LineRight();
            }
            else if (position.X < scrollMargin && scrollViewer.HorizontalOffset > 0)
            {
                scrollViewer.LineLeft();
            }
            else if (position.Y >= scrollViewer.ActualHeight - scrollMargin && scrollViewer.VerticalOffset < scrollViewer.ExtentHeight - scrollViewer.ViewportHeight)
            {
                scrollViewer.LineDown();
            }
            else if (position.Y < scrollMargin && scrollViewer.VerticalOffset > 0)
            {
                scrollViewer.LineUp();
            }
        }

        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (HitTestScrollBar(sender, e))
            {
                _dragInfo = null;
                return;
            }

            _dragInfo = new DragInfo(sender, e);

            var itemsControl = sender as ItemsControl;

            if (_dragInfo.VisualSourceItem == null || itemsControl == null || !itemsControl.CanSelectMultipleItems())
            {
                return;
            }

            var selectedItems = itemsControl.GetSelectedItems();

            if (selectedItems.Cast<object>().Contains(_dragInfo.SourceItem))
            {
                // TODO: Re-raise the suppressed event if the user didn't initiate a drag.
                e.Handled = true;
            }
        }

        private static void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragInfo = null;
        }

        private static void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragInfo == null)
            {
                return;
            }

            var dragStart = _dragInfo.DragStartPosition;
            var position = e.GetPosition(null);

            if (!(Math.Abs(position.X - dragStart.X) > SystemParameters.MinimumHorizontalDragDistance) &&
                !(Math.Abs(position.Y - dragStart.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                return;
            }

            var dragHandler = GetDragHandler(_dragInfo.VisualSource);

            if (dragHandler != null)
            {
                dragHandler.StartDrag(_dragInfo);
            }
            else
            {
                DefaultDragHandler.StartDrag(_dragInfo);
            }

            if (_dragInfo.Effects == DragDropEffects.None || _dragInfo.Data == null)
            {
                return;
            }

            var data = new DataObject(Format.Name, _dragInfo.Data);
            System.Windows.DragDrop.DoDragDrop(_dragInfo.VisualSource, data, _dragInfo.Effects);
            _dragInfo = null;
        }

        private static void OnPreviewDragEnter(object sender, DragEventArgs e)
        {
            OnPreviewDragOver(sender, e);
        }

        private static void OnPreviewDragLeave(object sender, DragEventArgs e)
        {
            DragAdorner = null;
            DropTargetAdorner = null;
        }

        private static void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            var dropInfo = new DropInfo(sender, e, _dragInfo, Format.Name);
            var dropHandler = GetDropHandler((UIElement)sender);

            if (dropHandler != null)
            {
                dropHandler.DragOver(dropInfo);
            }
            else
            {
                DefaultDropHandler.DragOver(dropInfo);
            }

            UpdateDragAdorner(dropInfo, e);

            var target = sender as ItemsControl;
            if (target != null)
            {
                UpdateTargetDropAdorner(target, dropInfo);
            }

            e.Effects = dropInfo.Effects;
            e.Handled = true;

            Scroll((DependencyObject)sender, e);
        }

        private static void UpdateTargetDropAdorner(DependencyObject target, DropInfo dropInfo)
        {
            UIElement adornedElement = target.GetVisualDescendent<ItemsPresenter>();

            if (dropInfo.DropTargetAdorner == null)
            {
                DropTargetAdorner = null;
            }
            else if (!dropInfo.DropTargetAdorner.IsInstanceOfType(DropTargetAdorner))
            {
                DropTargetAdorner = DropTargetAdorner.Create(dropInfo.DropTargetAdorner, adornedElement);
            }

            if (DropTargetAdorner == null)
            {
                return;
            }

            DropTargetAdorner.DropInfo = dropInfo;
            DropTargetAdorner.InvalidateVisual();
        }

        private static void UpdateDragAdorner(DropInfo dropInfo, DragEventArgs e)
        {
            if (dropInfo.Effects == DragDropEffects.None)
            {
                DragAdorner = null;
                return;
            }

            if (DragAdorner == null && _dragInfo != null)
            {
                var sourceElement = e.Source as UIElement;

                CreateDragAdorner(sourceElement);
            }

            if (DragAdorner == null)
            {
                return;
            }

            DragAdorner.MousePosition = e.GetPosition(DragAdorner.AdornedElement);
            DragAdorner.InvalidateVisual();
        }

        private static void OnPreviewDrop(object sender, DragEventArgs e)
        {
            var dropInfo = new DropInfo(sender, e, _dragInfo, Format.Name);
            var dropHandler = GetDropHandler((UIElement)sender);

            DragAdorner = null;
            DropTargetAdorner = null;

            if (dropHandler != null)
            {
                dropHandler.Drop(dropInfo);
            }
            else
            {
                DefaultDropHandler.Drop(dropInfo);
            }

            e.Handled = true;
        }
    }
}