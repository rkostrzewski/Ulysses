using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Utilities;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Utilities
{
    public partial class SleeperCustomizationView
    {
        public SleeperCustomizationView(ISleeperCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}