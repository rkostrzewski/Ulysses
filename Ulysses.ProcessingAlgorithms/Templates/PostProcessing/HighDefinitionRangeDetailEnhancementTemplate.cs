using System;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing;

namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public class HighDefinitionRangeDetailEnhancementTemplate : BaseImageProcessingAlgorithmTemplate
    {
        private ImageModel _imageModel;

        public HighDefinitionRangeDetailEnhancementTemplate()
        {
            BilateralFilterTemplate = new BilateralFilterTemplate();
        }

        public override ImageModel ImageModel
        {
            get
            {
                return _imageModel;
            }
            set
            {
                _imageModel = value;
                BilateralFilterTemplate.ImageModel = value;
            }
        }

        public override Type AlgorithmType => typeof (HighDefinitionRangeDetailEnhancement);
        public override ImageProcessingAlgorithmGroup Group { get; }

        public uint BinaryHistogramThreshold { get; set; }
        public double DetailsMinGain { get; set; }
        public double DetailsMaxGain { get; set; }
        public BilateralFilterTemplate BilateralFilterTemplate { get; }
    }
}