using System;
using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionTemplate : BaseNonUniformityCorrectionTemplate
    {
        public TwoPointNonUniformityCorrectionTemplate()
        {
            Algorithm = ImageProcessingAlgorithmType.TwoPointNonUniformityAlgorithm;
        }

        public override ImageProcessingAlgorithmType Algorithm { get; }

        public override ProcessingAlgorithms.Factories.IImageProcessingAlgorithmTemplate Template { get; set; }

        public override string NonUniformityModelFilePath { get; set; }

        public override bool UseNonUniformityModel
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }
    }
}