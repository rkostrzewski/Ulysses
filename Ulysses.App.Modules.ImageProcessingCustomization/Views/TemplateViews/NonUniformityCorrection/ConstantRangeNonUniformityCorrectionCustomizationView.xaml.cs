using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection
{
    public partial class ConstantRangeNonUniformityCorrectionCustomizationView
    {
        public ConstantRangeNonUniformityCorrectionCustomizationView(IConstantRangeNonUniformityCorrectionCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
