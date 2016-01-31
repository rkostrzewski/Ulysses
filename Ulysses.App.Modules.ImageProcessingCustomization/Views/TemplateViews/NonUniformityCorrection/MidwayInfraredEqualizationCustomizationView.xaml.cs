using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection
{
    public partial class MidwayInfraredEqualizationCustomizationView
    {
        public MidwayInfraredEqualizationCustomizationView(IMidwayInfraredEqualizationCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
