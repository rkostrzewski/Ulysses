using System;
using Prism.Regions;
using Ulysses.App.Modules.Content;
using Ulysses.App.Modules.Content.ImageDisplay.Views;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views;
using Ulysses.App.Regions;

namespace Ulysses.App.Modules.ControlPanel.Commands
{
    public class ChangeContentRegionCommand : IChangeContentRegionCommand
    {
        private readonly IRegionManager _regionManager;
        private readonly string _contentRegionName = ApplicationRegions.ContentRegion.ToString();

        public ChangeContentRegionCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object viewToChangeTo)
        {
            var region = _regionManager.Regions[_contentRegionName];

            switch ((ContentViews)viewToChangeTo)
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

        public event EventHandler CanExecuteChanged;
    }
}