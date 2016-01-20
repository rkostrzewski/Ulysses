using Prism.Regions;
using Ulysses.App.Modules.Regions;
using Ulysses.App.Regions;
using Ulysses.App.Utils.Commands.Regions;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeContentRegionsViewCommand : ChangeRegionsViewCommand<ContentRegionView>, IChangeContentRegionsViewCommand
    {
        public ChangeContentRegionsViewCommand(IRegionManager regionManager) : base(regionManager, ApplicationRegion.ContentRegion.ToString())
        {
        }
    }
}