using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.Misc;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Misc
{
    public class SaveImageCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<SaveImageTemplate>, ISaveImageCustomizationViewModel
    {
        public SaveImageCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public bool SaveAsRaw
        {
            get
            {
                return ChainElement.SaveAsRaw;
            }
            set
            {
                if (ChainElement.SaveAsRaw == value)
                {
                    return;
                }

                ChainElement.SaveAsRaw = value;
                OnPropertyChanged();
            }
        }

        public bool SaveAsPng
        {
            get
            {
                return ChainElement.SaveAsPng;
            }
            set
            {
                if (ChainElement.SaveAsPng == value)
                {
                    return;
                }

                ChainElement.SaveAsPng = value;
                OnPropertyChanged();
            }
        }
    }
}
