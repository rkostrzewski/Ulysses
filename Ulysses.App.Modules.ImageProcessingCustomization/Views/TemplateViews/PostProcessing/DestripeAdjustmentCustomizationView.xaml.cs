using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing
{
    public partial class DestripeCustomizationView
    {
        public DestripeCustomizationView(IDestripeCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
