using Ulysses.App.Modules.ImageProcessingCustomization.Commands;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public interface ITwoPointNonUniformityCorrectionCustomizationViewModel
    {
        ISelectFileCommand SelectFileCommand { get; }
        string NonUniformityModelFilePath { get; set; }
    }
}