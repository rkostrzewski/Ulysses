using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing
{
    public partial class GammaAdjustmentCustomizationView
    {
        public GammaAdjustmentCustomizationView(IGammaAdjustmentCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
