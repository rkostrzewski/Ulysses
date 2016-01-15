using Prism.Regions;
using Ulysses.App.Regions;
using Ulysses.App.Utils.Commands.Region;

namespace Ulysses.App.Modules.ControlPanel.Commands
{
    public class ChangeContentRegionViewCommand : ChangeRegionViewCommand, IChangeContentRegionViewCommand
    {
        public ChangeContentRegionViewCommand(IRegionManager regionManager) : base(regionManager, ApplicationRegions.ContentRegion)
        {
        }
    }
}