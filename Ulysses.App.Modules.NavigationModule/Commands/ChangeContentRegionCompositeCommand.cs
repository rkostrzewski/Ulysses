using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Regions;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeContentRegionCompositeCommand : CompositeCommand<ContentRegionView>, IChangeContentRegionCompositeCommand
    {
        public ChangeContentRegionCompositeCommand(IChangeContentRegionsViewCommand changeContentRegionsViewCommand,
                                                   IChangeCurrentRegionInNavigationPanelCommand changeCurrentRegionInNavigationPanelCommand)
        {
            RegisterCommand(changeContentRegionsViewCommand);
            RegisterCommand(changeCurrentRegionInNavigationPanelCommand);
        }
    }
}