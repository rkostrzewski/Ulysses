using Ulysses.App.Core.Regions;
using Ulysses.App.Core.ViewModels;

namespace Ulysses.App.Modules.NavigationModule.Models
{
    public class NavigationPanelState : NotifyPropertyChanged, INavigationPanelState
    {
        private ContentRegionView _currentContentRegionView;

        public ContentRegionView CurrentContentRegionView
        {
            get
            {
                return _currentContentRegionView;
            }
            set
            {
                if (_currentContentRegionView == value)
                {
                    return;
                }

                _currentContentRegionView = value;
                OnPropertyChanged();
            }
        }
    }
}