using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Regions;

namespace Ulysses.App.Modules.Navigation.ViewModels
{
    public interface INavigationPanelViewModel
    {
        ICompositeCommand<ContentRegionView> ChangeContentRegionsViewCommand { get; }
        ContentRegionView CurrentContentRegionView { get; }
    }
}