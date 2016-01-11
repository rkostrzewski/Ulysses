using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Ulysses.App.Controls.Workflows.Items.Adorners;

namespace Ulysses.App.Controls.Workflows.Items
{
    public class SelectedItemDecorator : Control
    {
        private static readonly DependencyProperty ShowDecoratorProperty = DependencyProperty.Register(nameof(ShowDecorator),
                                                                                                       typeof (bool),
                                                                                                       typeof (SelectedItemDecorator),
                                                                                                       new FrameworkPropertyMetadata(false, OnChanged));

        private Adorner _adorner;

        public SelectedItemDecorator()
        {
            Unloaded += OnUnloaded;
        }

        public bool ShowDecorator
        {
            get
            {
                return (bool)GetValue(ShowDecoratorProperty);
            }
            set
            {
                SetValue(ShowDecoratorProperty, value);
            }
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var decorator = (SelectedItemDecorator)d;
            var showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_adorner == null)
            {
                return;
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer?.Remove(_adorner);

            _adorner = null;
        }

        private void HideAdorner()
        {
            if (_adorner == null)
            {
                return;
            }

            _adorner.Visibility = Visibility.Hidden;
        }

        private void ShowAdorner()
        {
            if (_adorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                var workflowItem = DataContext as ContentControl;
                if (adornerLayer == null || workflowItem == null)
                {
                    return;
                }

                _adorner = new SelectedItemAdorner(workflowItem);
                adornerLayer.Add(_adorner);
            }

            _adorner.Visibility = Visibility.Visible;
        }
    }
}