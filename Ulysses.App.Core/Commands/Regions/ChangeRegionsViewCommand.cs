using System;
using System.Globalization;
using System.Linq;
using Prism.Regions;
using Ulysses.App.Core.Exceptions;

namespace Ulysses.App.Core.Commands.Regions
{
    public class ChangeRegionsViewCommand<T> : Command<T>, IChangeRegionsViewCommand<T> where T : IConvertible
    {
        private readonly string _parentRegionName;
        private readonly IRegionManager _regionManager;

        public ChangeRegionsViewCommand(IRegionManager regionManager, string parentRegionName)
        {
            _regionManager = regionManager;
            _parentRegionName = parentRegionName;
        }

        public override bool CanExecute(T targetViewName)
        {
            return _regionManager.Regions.ContainsRegionWithName(_parentRegionName) &&
                   _regionManager.Regions[_parentRegionName].Views.Any(v => v.GetType().Name == targetViewName.ToString(CultureInfo.InvariantCulture));
        }

        public override void Execute(T targetViewName)
        {
            if (!CanExecute(targetViewName))
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var region = _regionManager.Regions[_parentRegionName];

            region.RequestNavigate(targetViewName.ToString(CultureInfo.InvariantCulture));
        }
    }
}