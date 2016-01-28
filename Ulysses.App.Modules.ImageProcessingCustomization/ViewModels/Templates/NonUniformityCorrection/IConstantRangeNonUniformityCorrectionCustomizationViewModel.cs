namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public interface IConstantRangeNonUniformityCorrectionCustomizationViewModel
    {
        ushort RangeMinimum { get; set; }
        ushort RangeMaximum { get; set; }
        ushort AmountOfProcessedImagesToStopCalibration { get; set; }
    }
}