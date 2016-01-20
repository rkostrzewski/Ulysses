using System.ComponentModel;
using Ulysses.App.Core.Regions;

namespace Ulysses.App.Modules.NavigationModule.Models
{
    public interface INavigationPanelState
    {
        ContentRegionView CurrentContentRegionView { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}