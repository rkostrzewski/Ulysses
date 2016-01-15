using System;
using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.TemplateViewModels.NonUniformityCorrection
{
    public class TwoPointNonUniformityTemplateViewModel : BaseNonUniformityTemplateViewModel
    {
        public TwoPointNonUniformityTemplateViewModel()
        {
            Algorithm = ImageProcessingAlgorithm.TwoPointNonUniformityAlgorithm;
        }

        public override ImageProcessingAlgorithm Algorithm { get; }

        public override IImageProcessingAlgorithmTemplate Template { get; set; }

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