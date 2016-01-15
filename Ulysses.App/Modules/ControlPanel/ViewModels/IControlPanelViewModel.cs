using Ulysses.App.Modules.ControlPanel.Commands;

namespace Ulysses.App.Modules.ControlPanel.ViewModels
{
    public interface IControlPanelViewModel
    {
        IChangeContentRegionViewCommand ChangeContentRegionViewCommand { get; }
    }
}