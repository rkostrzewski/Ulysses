using Prism.Regions;
using Ulysses.App.Core.Commands.Regions;
using Ulysses.App.Core.Regions;

namespace Ulysses.App.Modules.NavigationModule.Commands
{
    public class ChangeContentRegionsViewCommand : ChangeRegionsViewCommand<ContentRegionView>, IChangeContentRegionsViewCommand
    {
        public ChangeContentRegionsViewCommand(IRegionManager regionManager) : base(regionManager, ApplicationRegion.ContentRegion.ToString())
        {
        }
    }
}