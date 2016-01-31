using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class BilateralFilterCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<BilateralFilterTemplate>, IBilateralFilterCustomizationViewModel
    {
        public BilateralFilterCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public ushort SpatialKernel
        {
            get
            {
                return ChainElement.SpatialKernel;
            }
            set
            {
                if (Math.Abs(ChainElement.SpatialKernel - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.SpatialKernel = value;
                OnPropertyChanged();
            }
        }

        public ushort RangeKernel
        {
            get
            {
                return ChainElement.RangeKernel;
            }
            set
            {
                if (Math.Abs(ChainElement.RangeKernel - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.RangeKernel = value;
                OnPropertyChanged();
            }
        }
    }
}