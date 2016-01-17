using System;
using Ulysses.App.Modules.Navigation.ViewModels;
using Ulysses.App.Modules.Regions;
using Ulysses.App.Utils.Commands;

namespace Ulysses.App.Modules.Navigation.Commands
{
    public class ChangeCurrentRegionInNavigationPanelCommand : Command<ContentRegionView>, IChangeCurrentRegionInNavigationPanelCommand
    {
        private readonly INavigationPanelViewModel _viewModel;

        public ChangeCurrentRegionInNavigationPanelCommand(INavigationPanelViewModel navigationPanelViewModel)
        {
            _viewModel = navigationPanelViewModel;
        }

        public override void Execute(ContentRegionView parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException();
            }

            _viewModel.SelectedContentRegionView = parameter;
        }

        public override bool CanExecute(ContentRegionView parameter)
        {
            return _viewModel != null;
        }
    }
}