namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public interface IHighDefinitionRangeDetailEnhancementCustomizationViewModel
    {
        ushort BilateralFilterSpatialKernel { get; set; }
        ushort BilateralFilterRangeKernel { get; set; }
        uint BinaryHistogramThreshold { get; set; }
        double DetailsMinGain { get; set; }
        double DetailsMaxGain { get; set; }
    }
}