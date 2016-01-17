using Prism.Regions;
using Ulysses.App.Modules.Regions;
using Ulysses.App.Regions;
using Ulysses.App.Utils.Commands.Region;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeContentRegionViewCommand : ChangeRegionViewCommand<ContentRegionView>, IChangeContentRegionViewCommand
    {
        public ChangeContentRegionViewCommand(IRegionManager regionManager) : base(regionManager, ApplicationRegion.ContentRegion.ToString())
        {
        }
    }
}