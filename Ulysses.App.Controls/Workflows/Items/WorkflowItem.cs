using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ulysses.App.Controls.Workflows.DesignerCanvas;
using Ulysses.App.Controls.Workflows.Items.Thumbs;
using Ulysses.App.Controls.Workflows.Templates;

namespace Ulysses.App.Controls.Workflows.Items
{
    [TemplatePart(Name = TemplateNames.DragThumb, Type = typeof (DragThumb))]
    [TemplatePart(Name = TemplateNames.SelectedItemDecorator, Type = typeof (Control))]
    [TemplatePart(Name = TemplateNames.ConnectorDecorator, Type = typeof (Control))]
    [TemplatePart(Name = TemplateNames.ContentPresenter, Type = typeof (ContentPresenter))]
    public class WorkflowItem : ContentControl, ISelectable
    {
        private static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected),
                                                                                                    typeof (bool),
                                                                                                    typeof (WorkflowItem),
                                                                                                    new FrameworkPropertyMetadata(false));

        private static readonly DependencyProperty DragThumbTemplateProperty = DependencyProperty.RegisterAttached(TemplatePropertyNames.DragThumbTemplate,
                                                                                                                   typeof (ControlTemplate),
                                                                                                                   typeof (WorkflowItem));

        private static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached(TemplatePropertyNames.ConnectorDecoratorTemplate, typeof (ControlTemplate), typeof (WorkflowItem));

        private static readonly DependencyProperty IsDragConnectionOverProperty = DependencyProperty.Register(nameof(IsDragConnectionOver),
                                                                                                              typeof (bool),
                                                                                                              typeof (WorkflowItem),
                                                                                                              new FrameworkPropertyMetadata(false));

        static WorkflowItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WorkflowItem), new FrameworkPropertyMetadata(typeof (WorkflowItem)));
        }

        public WorkflowItem()
        {
            Loaded += OnLoaded;
        }

        public bool IsDragConnectionOver
        {
            get
            {
                return (bool)GetValue(IsDragConnectionOverProperty);
            }
            set
            {
                SetValue(IsDragConnectionOverProperty, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        public static ControlTemplate GetDragThumbTemplate(DependencyObject element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(DependencyObject element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            var designer = VisualTreeHelper.GetParent(this) as WorkflowDesignerCanvas;
            e.Handled = false;

            if (designer == null)
            {
                return;
            }

            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
            {
                if (IsSelected)
                {
                    IsSelected = false;
                    designer.SelectedItems.Remove(this);
                    return;
                }

                IsSelected = true;
                designer.SelectedItems.Add(this);
            }

            if (IsSelected)
            {
                return;
            }

            IsSelected = true;
            designer.DeselectAll();
            designer.SelectedItems.Clear();
            designer.SelectedItems.Add(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var contentPresenter = Template?.FindName(TemplateNames.ContentPresenter, this) as ContentPresenter;

            if (contentPresenter == null)
            {
                return;
            }

            var contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;

            if (contentVisual == null)
            {
                return;
            }

            var controlsWithTemplates = new List<ControlWithTemplate>
            {
                new ControlWithTemplate { Control = Template.FindName(TemplateNames.DragThumb, this) as DragThumb, Template = GetDragThumbTemplate(contentVisual) },
                new ControlWithTemplate
                {
                    Control = Template.FindName(TemplateNames.ContentPresenter, this) as Control,
                    Template = GetConnectorDecoratorTemplate(contentVisual)
                }
            };

            foreach (var controlWithTemplate in controlsWithTemplates.Where(cwt => cwt.Control != null && cwt.Template != null))
            {
                controlWithTemplate.Control.Template = controlWithTemplate.Template;
            }
        }
    }
}