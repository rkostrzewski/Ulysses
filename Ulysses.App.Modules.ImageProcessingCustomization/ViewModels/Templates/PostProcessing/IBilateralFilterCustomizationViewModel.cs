namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public interface IBilateralFilterCustomizationViewModel
    {
        ushort SpatialKernel { get; set; }
        ushort RangeKernel { get; set; }
    }
}