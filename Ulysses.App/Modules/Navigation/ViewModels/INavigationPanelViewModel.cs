using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Regions;
using Ulysses.App.Utils.Commands;

namespace Ulysses.App.Modules.Navigation.ViewModels
{
    public interface INavigationPanelViewModel
    {
        ICompositeCommand<ContentRegionView> ChangeContentRegionViewCommand { get; }
        ContentRegionView SelectedContentRegionView { get; set; }
    }
}