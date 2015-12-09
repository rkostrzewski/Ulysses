using System;
using Prism.Regions;
using Ulysses.App.Modules.Content;
using Ulysses.App.Modules.Content.ImageDisplay.Views;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views;
using Ulysses.App.Regions;
using Ulysses.App.Utils;

namespace Ulysses.App.Modules.ControlPanel.Commands
{
    public class ChangeContentRegionViewCommand : Command<ContentViews>, IChangeContentRegionCommand
    {
        private readonly IRegionManager _regionManager;
        private readonly string _contentRegionName = ApplicationRegions.ContentRegion.ToString();

        public ChangeContentRegionViewCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public override event EventHandler CanExecuteChanged;

        public override bool CanExecute(ContentViews parameter)
        {
            return _regionManager.Regions.ContainsRegionWithName(_contentRegionName);
        }

        public override void Execute(ContentViews viewToChangeTo)
        {
            var region = _regionManager.Regions[_contentRegionName];

            switch (viewToChangeTo)
            {
                case ContentViews.ImageDisplay:
                    region.RequestNavigate(nameof(ImageDisplayView));
                    return;
                case ContentViews.ImageProcessingCustomization:
                    region.RequestNavigate(nameof(ImageProcessingCustomizationView));
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}