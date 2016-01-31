using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing
{
    public partial class HighDefinitionRangeDetailEnhancementCustomizationView
    {
        public HighDefinitionRangeDetailEnhancementCustomizationView(IHighDefinitionRangeDetailEnhancementCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
