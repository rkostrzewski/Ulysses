using System;
using System.Linq;
using Prism.Regions;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Commands.Regions;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public class NavigateToProcessingChainElementCustomizationViewCommand : Command<IProcessingChainElementTemplate>
    {
        private readonly string _parentRegionName;
        private readonly IRegionManager _regionManager;
        private readonly IViewLocator<IProcessingChainElementTemplate> _viewLocator;

        public NavigateToProcessingChainElementCustomizationViewCommand(IRegionManager regionManager,
                                                                        string parentRegionName,
                                                                        IViewLocator<IProcessingChainElementTemplate> viewLocator)
        {
            _regionManager = regionManager;
            _parentRegionName = parentRegionName;
            _viewLocator = viewLocator;
        }

        public override bool CanExecute(IProcessingChainElementTemplate processingChainElementTemplate)
        {
            if (!_regionManager.Regions.ContainsRegionWithName(_parentRegionName))
            {
                return false;
            }

            var viewType = _viewLocator.GetViewType(processingChainElementTemplate);

            return _regionManager.Regions[_parentRegionName].Views.Any(v => v.GetType() == viewType);
        }

        public override void Execute(IProcessingChainElementTemplate processingChainElementTemplate)
        {
            if (!CanExecute(processingChainElementTemplate))
            {
                throw new InvalidOperationException();
            }

            var region = _regionManager.Regions[_parentRegionName];
            var targetViewType = _viewLocator.GetViewType(processingChainElementTemplate);
            var navigationParameters = new NavigationParameters { { nameof(IProcessingChainElementTemplate.Id), processingChainElementTemplate.Id } };

            region.RequestNavigate(targetViewType.Name, navigationParameters);
        }
    }
}