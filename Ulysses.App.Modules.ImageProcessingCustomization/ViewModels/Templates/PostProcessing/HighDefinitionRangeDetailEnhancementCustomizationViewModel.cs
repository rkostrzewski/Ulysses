using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing
{
    public class HighDefinitionRangeDetailEnhancementCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<HighDefinitionRangeDetailEnhancementTemplate>, IHighDefinitionRangeDetailEnhancementCustomizationViewModel
    {
        public HighDefinitionRangeDetailEnhancementCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public ushort BilateralFilterSpatialKernel
        {
            get
            {
                return ChainElement.BilateralFilterTemplate.SpatialKernel;
            }
            set
            {
                if (Math.Abs(ChainElement.BilateralFilterTemplate.SpatialKernel - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.BilateralFilterTemplate.SpatialKernel = value;
                OnPropertyChanged();
            }
        }

        public ushort BilateralFilterRangeKernel
        {
            get
            {
                return ChainElement.BilateralFilterTemplate.RangeKernel;
            }
            set
            {
                if (Math.Abs(ChainElement.BilateralFilterTemplate.RangeKernel - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.BilateralFilterTemplate.RangeKernel = value;
                OnPropertyChanged();
            }
        }

        public uint BinaryHistogramThreshold
        {
            get
            {
                return ChainElement.BinaryHistogramThreshold;
            }
            set
            {
                if (ChainElement.BinaryHistogramThreshold == value)
                {
                    return;
                }

                ChainElement.BinaryHistogramThreshold = value;
                OnPropertyChanged();
            }
        }

        public double DetailsMinGain
        {
            get
            {
                return ChainElement.DetailsMinGain;
            }
            set
            {
                if (Math.Abs(ChainElement.DetailsMinGain - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.DetailsMinGain = value;
                OnPropertyChanged();
            }
        }

        public double DetailsMaxGain
        {
            get
            {
                return ChainElement.DetailsMaxGain;
            }
            set
            {
                if (Math.Abs(ChainElement.DetailsMaxGain - value) < double.Epsilon)
                {
                    return;
                }

                ChainElement.DetailsMaxGain = value;
                OnPropertyChanged();
            }
        }
    }
}
