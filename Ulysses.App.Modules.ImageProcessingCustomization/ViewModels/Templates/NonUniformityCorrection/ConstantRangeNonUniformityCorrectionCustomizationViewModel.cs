using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class ConstantRangeNonUniformityCorrectionCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<ConstantRangeNonUniformityCorrectionTemplate>, IConstantRangeNonUniformityCorrectionCustomizationViewModel
    {
        public ConstantRangeNonUniformityCorrectionCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public ushort RangeMinimum
        {
            get
            {
                return ChainElement.RangeMinimum;
            }
            set
            {
                if (ChainElement.RangeMinimum == value)
                {
                    return;
                }

                ChainElement.RangeMinimum = value;
                OnPropertyChanged();
            }
        }

        public ushort RangeMaximum
        {
            get
            {
                return ChainElement.RangeMaximum;
            }
            set
            {
                if (ChainElement.RangeMaximum == value)
                {
                    return;
                }

                ChainElement.RangeMaximum = value;
                OnPropertyChanged();
            }
        }

        public ushort AmountOfProcessedImagesToStopCalibration
        {
            get
            {
                return ChainElement.AmountOfProcessedImagesToStopCalibration;
            }
            set
            {
                if (ChainElement.AmountOfProcessedImagesToStopCalibration == value)
                {
                    return;
                }

                ChainElement.AmountOfProcessedImagesToStopCalibration = value;
                OnPropertyChanged();
            }
        }
    }
}