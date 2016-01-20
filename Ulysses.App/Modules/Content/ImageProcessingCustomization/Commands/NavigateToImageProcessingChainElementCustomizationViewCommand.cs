using System;
using System.Linq;
using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models;
using Ulysses.App.Utils.Commands;
using Ulysses.App.Utils.Commands.Regions;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Commands
{
    public class NavigateToImageProcessingChainElementCustomizationViewCommand : Command<IImageProcessingChainElement>
    {
        private readonly string _parentRegionName;
        private readonly IRegionManager _regionManager;
        private readonly IViewLocator<IImageProcessingChainElement> _viewLocator;

        public NavigateToImageProcessingChainElementCustomizationViewCommand(IRegionManager regionManager,
                                                                             string parentRegionName,
                                                                             IViewLocator<IImageProcessingChainElement> viewLocator)
        {
            _regionManager = regionManager;
            _parentRegionName = parentRegionName;
            _viewLocator = viewLocator;
        }

        public override bool CanExecute(IImageProcessingChainElement processingChainElement)
        {
            if (!_regionManager.Regions.ContainsRegionWithName(_parentRegionName))
            {
                return false;
            }

            var viewType = _viewLocator.GetViewType(processingChainElement);

            return _regionManager.Regions[_parentRegionName].Views.Any(v => v.GetType() == viewType);
        }

        public override void Execute(IImageProcessingChainElement processingChainElement)
        {
            if (!CanExecute(processingChainElement))
            {
                throw new InvalidOperationException();
            }

            var region = _regionManager.Regions[_parentRegionName];
            var targetViewType = _viewLocator.GetViewType(processingChainElement);
            var navigationParameters = new NavigationParameters { { nameof(IImageProcessingChainElement.Id), processingChainElement.Id } };

            region.RequestNavigate(targetViewType.Name, navigationParameters);
        }
    }
}