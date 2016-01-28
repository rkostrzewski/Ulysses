using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class BaseAdjustmentCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<BaseAdjustmentTemplate>
    {
        public BaseAdjustmentCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public int AdjustmentValue
        {
            get
            {
                return ChainElement.AdjustmentValue;
            }
            set
            {
                if (ChainElement.AdjustmentValue == value)
                {
                    return;
                }

                ChainElement.AdjustmentValue = value;
                OnPropertyChanged();
            }
        }
    }
}