using Ulysses.App.Modules.ControlPanel.ViewModels;

namespace Ulysses.App.Modules.ControlPanel.Views
{
    public partial class ControlPanelView
    {
        public ControlPanelView(IControlPanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
