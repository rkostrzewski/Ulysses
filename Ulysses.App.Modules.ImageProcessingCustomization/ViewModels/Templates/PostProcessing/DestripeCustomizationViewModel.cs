using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class DestripeCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<DestripeTemplate>, IDestripeCustomizationViewModel
    {
        public DestripeCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public ushort Radius
        {
            get
            {
                return ChainElement.Radius;
            }
            set
            {
                if (ChainElement.Radius == value)
                {
                    return;
                }

                ChainElement.Radius = value;
                OnPropertyChanged();
            }
        }
    }
}