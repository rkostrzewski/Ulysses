using System.ComponentModel;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Regions;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.Models;

namespace Ulysses.App.Modules.Navigation.ViewModels
{
    public class NavigationPanelViewModel : NotifyPropertyChanged, INavigationPanelViewModel
    {
        private readonly INavigationPanelState _navigationPanelState;

        public NavigationPanelViewModel(INavigationPanelState navigationPanelState, IChangeContentRegionCompositeCommand changeContentRegionsViewCommand)
        {
            _navigationPanelState = navigationPanelState;
            _navigationPanelState.PropertyChanged += OnNavigationPanelStatePropertyChanged;

            ChangeContentRegionsViewCommand = changeContentRegionsViewCommand;
        }

        public ICompositeCommand<ContentRegionView> ChangeContentRegionsViewCommand { get; }

        public ContentRegionView CurrentContentRegionView => _navigationPanelState.CurrentContentRegionView;

        private void OnNavigationPanelStatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _navigationPanelState && e.PropertyName == nameof(INavigationPanelState.CurrentContentRegionView))
            {
                OnPropertyChanged(nameof(CurrentContentRegionView));
            }
        }
    }
}