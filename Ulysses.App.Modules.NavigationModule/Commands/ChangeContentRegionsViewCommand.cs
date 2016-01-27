using Prism.Events;
using Prism.Regions;
using Ulysses.App.Core.Commands.Regions;
using Ulysses.App.Core.Regions;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeContentRegionsViewCommand : ChangeRegionsViewCommand<ContentRegionView>, IChangeContentRegionsViewCommand
    {
        public ChangeContentRegionsViewCommand(IRegionManager regionManager, IEventAggregator eventAggregator)
            : base(regionManager, ApplicationRegion.ContentRegion.ToString())
        {
        }
    }
}