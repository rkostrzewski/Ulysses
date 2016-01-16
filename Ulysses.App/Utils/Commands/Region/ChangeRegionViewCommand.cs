using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Prism.Regions;

namespace Ulysses.App.Utils.Commands.Region
{
    public class ChangeRegionViewCommand : Command<string>, IChangeRegionViewCommand
    { 
        private readonly string _parentRegionName;
        private readonly IRegionManager _regionManager;

        public ChangeRegionViewCommand(IRegionManager regionManager, string parentRegionName)
        {
            _regionManager = regionManager;
            _parentRegionName = parentRegionName;
        }

        public override bool CanExecute(string targetViewName)
        {
            return _regionManager.Regions.ContainsRegionWithName(_parentRegionName) && _regionManager.Regions[_parentRegionName].Views.Any(v => v.GetType().Name == targetViewName);
        }

        public override void Execute(string targetViewName)
        {
            if (!CanExecute(targetViewName))
            {
                throw new InvalidOperationException();
            }

            var region = _regionManager.Regions[_parentRegionName];

            region.RequestNavigate(targetViewName);
        }
    }
}
