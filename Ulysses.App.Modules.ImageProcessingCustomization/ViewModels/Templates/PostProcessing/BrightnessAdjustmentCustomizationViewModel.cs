using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class BrightnessAdjustmentCustomizationViewModel : BaseAdjustmentCustomizationViewModel, IBrightnessAdjustmentCustomizationViewModel
    {
        public BrightnessAdjustmentCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }
    }
}