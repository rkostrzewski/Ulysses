using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Utilities
{
    public class SleeperCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<SleeperTemplate>, ISleeperCustomizationViewModel
    {
        public SleeperCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public int SleepTimeInMilliseconds
        {
            get
            {
                return ChainElement.SleepTimeInMilliseconds;
            }
            set
            {
                if (ChainElement.SleepTimeInMilliseconds == value)
                {
                    return;
                }

                ChainElement.SleepTimeInMilliseconds = value;
                OnPropertyChanged();
            }
        }
    }
}