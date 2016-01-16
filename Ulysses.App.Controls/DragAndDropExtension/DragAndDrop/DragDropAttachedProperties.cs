using System.Windows;

namespace Ulysses.App.Controls.DragAndDropExtension.DragAndDrop
{
    public static partial class DragDrop
    {
        private const string DragAdornerTemplatePropertyName = "DragAdornerTemplate";
        private const string DragHandlerPropertyName = "DragHandler";
        private const string DropHandlerPropertyName = "DropHandler";
        private const string IsDragSourcePropertyName = "IsDragSource";
        private const string IsDropTargetPropertyName = "IsDropTarget";
        private const string AdornmentOpacityPropertyName = "AdornmentOpacity";

        private static readonly DependencyProperty DragAdornerTemplateProperty = DependencyProperty.RegisterAttached(DragAdornerTemplatePropertyName,
                                                                                                                     typeof (DataTemplate),
                                                                                                                     typeof (DragDrop));

        private static readonly DependencyProperty DragHandlerProperty = DependencyProperty.RegisterAttached(DragHandlerPropertyName,
                                                                                                             typeof (IDragSource),
                                                                                                             typeof (DragDrop));

        private static readonly DependencyProperty DropHandlerProperty = DependencyProperty.RegisterAttached(DropHandlerPropertyName,
                                                                                                             typeof (IDropTarget),
                                                                                                             typeof (DragDrop));

        private static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached(IsDragSourcePropertyName,
                                                                                                              typeof (bool),
                                                                                                              typeof (DragDrop),
                                                                                                              new UIPropertyMetadata(false, OnIsDragSourceChanged));

        private static readonly DependencyProperty IsDropTargetProperty = DependencyProperty.RegisterAttached(IsDropTargetPropertyName,
                                                                                                              typeof (bool),
                                                                                                              typeof (DragDrop),
                                                                                                              new UIPropertyMetadata(false, OnIsDropTargetChanged));

        private static readonly DependencyProperty AdornmentOpacityProperty = DependencyProperty.RegisterAttached(AdornmentOpacityPropertyName,
                                                                                                                  typeof (double),
                                                                                                                  typeof (DragDrop));


        public static DataTemplate GetDragAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(DragAdornerTemplateProperty);
        }

        public static void SetDragAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(DragAdornerTemplateProperty, value);
        }

        public static bool GetIsDragSource(UIElement target)
        {
            return (bool)target.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(UIElement target, bool value)
        {
            target.SetValue(IsDragSourceProperty, value);
        }

        public static bool GetIsDropTarget(UIElement target)
        {
            return (bool)target.GetValue(IsDropTargetProperty);
        }

        public static void SetIsDropTarget(UIElement target, bool value)
        {
            target.SetValue(IsDropTargetProperty, value);
        }

        public static IDragSource GetDragHandler(UIElement target)
        {
            return (IDragSource)target.GetValue(DragHandlerProperty);
        }

        public static void SetDragHandler(UIElement target, IDragSource value)
        {
            target.SetValue(DragHandlerProperty, value);
        }

        public static IDropTarget GetDropHandler(UIElement target)
        {
            return (IDropTarget)target.GetValue(DropHandlerProperty);
        }

        public static void SetDropHandler(UIElement target, IDropTarget value)
        {
            target.SetValue(DropHandlerProperty, value);
        }

        public static double GetAdornmentOpacity(UIElement target)
        {
            return (double)target.GetValue(AdornmentOpacityProperty);
        }
    }
}