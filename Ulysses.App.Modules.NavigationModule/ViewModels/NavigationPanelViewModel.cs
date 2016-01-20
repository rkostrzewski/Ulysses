using System.ComponentModel;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Regions;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.NavigationModule.Commands;
using Ulysses.App.Modules.NavigationModule.Models;

namespace Ulysses.App.Modules.NavigationModule.ViewModels
{
    public class NavigationPanelViewModel : NotifyPropertyChanged, INavigationPanelViewModel
    {
        private readonly INavigationPanelState _navigationPanelState;

        public NavigationPanelViewModel(INavigationPanelState navigationPanelState, IChangeContentRegionsViewCommand changeContentRegionsViewCommand, IChangeCurrentRegionInNavigationPanelCommand changeCurrentRegionInNavigationPanelCommand)
        {
            _navigationPanelState = navigationPanelState;
            _navigationPanelState.PropertyChanged += OnNavigationPanelStatePropertyChanged;

            ChangeContentRegionsViewCommand = new CompositeCommand<ContentRegionView>();
            ChangeContentRegionsViewCommand.RegisterCommand(changeContentRegionsViewCommand);
            ChangeContentRegionsViewCommand.RegisterCommand(changeCurrentRegionInNavigationPanelCommand);
        }

        public ICompositeCommand<ContentRegionView> ChangeContentRegionsViewCommand { get; }

        public ContentRegionView CurrentContentRegionView
        {
            get
            {
                return _navigationPanelState.CurrentContentRegionView;
            }
            set
            {
                if (_navigationPanelState.CurrentContentRegionView == value)
                {
                    return;
                }

                _navigationPanelState.CurrentContentRegionView = value;
                OnPropertyChanged();
            }
        }

        private void OnNavigationPanelStatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _navigationPanelState && e.PropertyName == nameof(INavigationPanelState.CurrentContentRegionView))
            {
                OnPropertyChanged(nameof(CurrentContentRegionView));
            }
        }
    }
}