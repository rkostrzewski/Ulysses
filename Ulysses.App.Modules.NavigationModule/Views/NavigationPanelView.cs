using Ulysses.App.Modules.NavigationModule.ViewModels;

namespace Ulysses.App.Modules.NavigationModule.Views
{
    public partial class NavigationPanelView
    {
        public NavigationPanelView(INavigationPanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}