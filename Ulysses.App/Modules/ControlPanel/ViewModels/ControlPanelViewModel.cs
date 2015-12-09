using Prism.Regions;
using Ulysses.App.Modules.ControlPanel.Commands;

namespace Ulysses.App.Modules.ControlPanel.ViewModels
{
    public class ControlPanelViewModel : IControlPanelViewModel
    {
        public ControlPanelViewModel(IRegionManager regionManager)
        {
            ChangeContentRegionViewCommand = new ChangeContentRegionViewCommand(regionManager);
        }

        public IChangeContentRegionCommand ChangeContentRegionViewCommand { get; }
    }
}