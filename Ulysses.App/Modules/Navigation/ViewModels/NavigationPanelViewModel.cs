using System.Collections.Generic;
using Prism.Commands;
using Prism.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Regions;
using Ulysses.App.Utils.Commands;
using Ulysses.App.Utils.ViewModels;

namespace Ulysses.App.Modules.Navigation.ViewModels
{
    public class NavigationPanelViewModel : NotifyPropertyChanged, INavigationPanelViewModel
    {
        private ContentRegionView _selectedContentRegionView;

        public NavigationPanelViewModel(IRegionManager regionManager)
        {
            ChangeContentRegionViewCommand = new CompositeCommand<ContentRegionView>();
            ChangeContentRegionViewCommand.RegisterCommand(new ChangeContentRegionViewCommand(regionManager));
            ChangeContentRegionViewCommand.RegisterCommand(new ChangeCurrentRegionInNavigationPanelCommand(this));
            SelectedContentRegionView = ContentRegionView.ImageDisplayView;
        }

        public ICompositeCommand<ContentRegionView> ChangeContentRegionViewCommand { get; }

        public ContentRegionView SelectedContentRegionView
        {
            get
            {
                return _selectedContentRegionView;
            }
            set
            {
                if (_selectedContentRegionView == value)
                {
                    return;
                }

                _selectedContentRegionView = value;
                OnPropertyChanged();
            }
        }
    }
}