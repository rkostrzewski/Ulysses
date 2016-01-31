using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing
{
    public partial class BilateralFilterCustomizationView
    {
        public BilateralFilterCustomizationView(IBilateralFilterCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
