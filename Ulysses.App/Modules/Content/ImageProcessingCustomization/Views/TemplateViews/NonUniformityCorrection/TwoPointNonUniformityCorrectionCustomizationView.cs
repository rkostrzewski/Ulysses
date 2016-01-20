using System.Windows.Controls;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection
{
    /// <summary>
    ///     Interaction logic for Test1.xaml
    /// </summary>
    public partial class TwoPointNonUniformityCorrectionCustomizationView
    {
        public TwoPointNonUniformityCorrectionCustomizationView(ITwoPointNonUniformityCorrectionCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}