using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class MidwayInfraredEqualizationCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<MidwayInfraredEqualizationTemplate>, IMidwayInfraredEqualizationCustomizationViewModel
    {
        public MidwayInfraredEqualizationCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public double StandardDeviation
        {
            get
            {
                return ChainElement.StandardDeviation;
            }
            set
            {
                if (Math.Abs(ChainElement.StandardDeviation - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.StandardDeviation = value;
                OnPropertyChanged();
            }
        }
    }
}
