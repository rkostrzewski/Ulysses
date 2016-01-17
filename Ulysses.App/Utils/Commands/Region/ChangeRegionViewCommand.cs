using System;
using System.Linq;
using Prism.Regions;

namespace Ulysses.App.Utils.Commands.Region
{
    public class ChangeRegionViewCommand<T> : Command<T>, IChangeRegionViewCommand<T> where T : IConvertible
    { 
        private readonly string _parentRegionName;
        private readonly IRegionManager _regionManager;

        public ChangeRegionViewCommand(IRegionManager regionManager, string parentRegionName)
        {
            _regionManager = regionManager;
            _parentRegionName = parentRegionName;
        }

        public override bool CanExecute(T targetViewName)
        {
            return _regionManager.Regions.ContainsRegionWithName(_parentRegionName) && _regionManager.Regions[_parentRegionName].Views.Any(v => v.GetType().Name == targetViewName.ToString());
        }

        public override void Execute(T targetViewName)
        {
            if (!CanExecute(targetViewName))
            {
                throw new InvalidOperationException();
            }

            var region = _regionManager.Regions[_parentRegionName];

            region.RequestNavigate(targetViewName.ToString());
        }
    }
}
