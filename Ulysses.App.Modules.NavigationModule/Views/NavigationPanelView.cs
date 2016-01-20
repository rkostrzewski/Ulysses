using Ulysses.App.Modules.Navigation.ViewModels;

namespace Ulysses.App.Modules.Navigation.Views
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