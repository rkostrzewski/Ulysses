using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing
{
    public partial class ContrastAdjustmentCustomizationView
    {
        public ContrastAdjustmentCustomizationView(IContrastAdjustmentCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
