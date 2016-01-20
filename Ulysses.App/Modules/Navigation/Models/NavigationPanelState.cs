using Ulysses.App.Modules.Regions;
using Ulysses.App.Utils.ViewModels;

namespace Ulysses.App.Modules.Navigation.Models
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