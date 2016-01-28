using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class GammaAdjustmentCustomizationViewModel : BaseAdjustmentCustomizationViewModel, IGammaAdjustmentCustomizationViewModel
    {
        public GammaAdjustmentCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }
    }
}