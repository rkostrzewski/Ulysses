using Prism.Regions;
using Ulysses.App.Modules.ControlPanel.Commands;

namespace Ulysses.App.Modules.ControlPanel.ViewModels
{
    public class ControlPanelViewModel : IControlPanelViewModel
    {
        public ControlPanelViewModel(IRegionManager regionManager)
        {
            ChangeContentRegionCommand = new ChangeContentRegionCommand(regionManager);
        }

        public IChangeContentRegionCommand ChangeContentRegionCommand { get; private set; }
    }
}