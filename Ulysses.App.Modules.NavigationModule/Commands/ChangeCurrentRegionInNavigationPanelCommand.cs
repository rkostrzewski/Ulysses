using System;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Models;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeCurrentRegionInNavigationPanelCommand : Command<ContentRegionView>, IChangeCurrentRegionInNavigationPanelCommand
    {
        private readonly INavigationPanelState _navigationPanelState;

        public ChangeCurrentRegionInNavigationPanelCommand(INavigationPanelState navigationPanelState)
        {
            _navigationPanelState = navigationPanelState;
        }

        public override void Execute(ContentRegionView parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException();
            }

            _navigationPanelState.CurrentContentRegionView = parameter;
        }

        public override bool CanExecute(ContentRegionView parameter)
        {
            return _navigationPanelState != null;
        }
    }
}