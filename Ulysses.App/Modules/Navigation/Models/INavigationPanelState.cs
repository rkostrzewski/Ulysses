using System.ComponentModel;
using Ulysses.App.Modules.Regions;

namespace Ulysses.App.Modules.Navigation.Models
{
    public interface INavigationPanelState
    {
        ContentRegionView CurrentContentRegionView { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}