using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ulysses.App.Controls.DragAndDropExtension.Adorners
{
    public class DropTargetInsertionAdorner : DropTargetAdorner
    {
        private static readonly Pen Pen;
        private static readonly PathGeometry Triangle;
        protected static double ScaleFactor;

        static DropTargetInsertionAdorner()
        {
            const int triangleSize = 3;

            ScaleFactor = 1.0d;

            Pen = new Pen(Brushes.Gray, 2);
            Pen.Freeze();

            var firstLine = new LineSegment(new Point(0, -triangleSize), false);
            var secondLine = new LineSegment(new Point(0, triangleSize), false);
            firstLine.Freeze();
            secondLine.Freeze();

            var figure = new PathFigure { StartPoint = new Point(triangleSize, 0) };
            figure.Segments.Add(firstLine);
            figure.Segments.Add(secondLine);
            figure.Freeze();

            Triangle = new PathGeometry();
            Triangle.Figures.Add(figure);
            Triangle.Freeze();
        }

        public DropTargetInsertionAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var itemsControl = DropInfo.VisualTarget as ItemsControl;

            if (itemsControl == null)
            {
                return;
            }

            var itemParent = DropInfo.VisualTargetItem != null ? ItemsControl.ItemsControlFromItemContainer(DropInfo.VisualTargetItem) : itemsControl;

            var index = Math.Min(DropInfo.InsertIndex, itemParent.Items.Count - 1);
            var itemContainer = (UIElement)itemParent.ItemContainerGenerator.ContainerFromIndex(index);

            if (itemContainer == null)
            {
                return;
            }

            var itemRect = new Rect(itemContainer.TranslatePoint(new Point(), AdornedElement), itemContainer.RenderSize);
            
            Point startPoint, endPoint;
            double rotation = 0;
            int length;
            switch (DropInfo.VisualTargetOrientation)
            {
                case Orientation.Horizontal:
                    if (DropInfo.InsertIndex == itemParent.Items.Count)
                    {
                        itemRect.X += itemContainer.RenderSize.Width;
                    }
                    length = (int)((itemRect.Bottom - itemRect.Y) * ScaleFactor);
                    startPoint = new Point(itemRect.X, itemRect.Y + length);
                    endPoint = new Point(itemRect.X, itemRect.Bottom - length);
                    rotation = 90;
                    break;
                case Orientation.Vertical:
                    if (DropInfo.InsertIndex == itemParent.Items.Count)
                    {
                        itemRect.Y += itemContainer.RenderSize.Height;
                    }

                    length = (int)((itemRect.Right - itemRect.X) * ScaleFactor);
                    startPoint = new Point(itemRect.X, itemRect.Y + length);
                    endPoint = new Point(itemRect.Right, itemRect.Y - length);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            drawingContext.DrawLine(Pen, startPoint, endPoint);
            DrawTriangle(drawingContext, startPoint, rotation);
            DrawTriangle(drawingContext, endPoint, 180 + rotation);
        }

        private static void DrawTriangle(DrawingContext drawingContext, Point origin, double rotation)
        {
            drawingContext.PushTransform(new TranslateTransform(origin.X, origin.Y));
            drawingContext.PushTransform(new RotateTransform(rotation));

            drawingContext.DrawGeometry(Pen.Brush, null, Triangle);

            drawingContext.Pop();
            drawingContext.Pop();
        }
    }
}