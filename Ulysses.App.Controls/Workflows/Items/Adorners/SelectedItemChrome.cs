using System.Windows;
using System.Windows.Controls;

namespace Ulysses.App.Controls.Workflows.Items.Adorners
{
    public class SelectedItemChrome : Control
    {
        static SelectedItemChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectedItemChrome), new FrameworkPropertyMetadata(typeof(SelectedItemChrome)));
        }
    }
}