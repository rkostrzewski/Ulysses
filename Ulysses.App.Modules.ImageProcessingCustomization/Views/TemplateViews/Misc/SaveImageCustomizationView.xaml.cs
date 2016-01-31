using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Misc;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Misc
{
    public partial class SaveImageCustomizationView
    {
        public SaveImageCustomizationView(ISaveImageCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
