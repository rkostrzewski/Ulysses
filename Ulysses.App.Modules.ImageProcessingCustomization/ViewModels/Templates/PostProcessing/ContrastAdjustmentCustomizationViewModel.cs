using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class ContrastAdjustmentCustomizationViewModel : BaseAdjustmentCustomizationViewModel, IContrastAdjustmentCustomizationViewModel
    {
        public ContrastAdjustmentCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }
    }
}