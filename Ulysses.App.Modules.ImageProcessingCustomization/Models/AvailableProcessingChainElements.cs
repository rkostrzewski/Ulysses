using System.Collections;
using System.Collections.Generic;
using Ulysses.Core.Templates;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models
{
    public class AvailableProcessingChainElements : IAvailableProcessingChainElements
    {
        private static readonly IEnumerable<IProcessingChainElementTemplate> AvailableProcessingChainElementsSource = new List<IProcessingChainElementTemplate>
        {
            new TwoPointNonUniformityCorrectionTemplate(),
            new ConstantRangeNonUniformityCorrectionTemplate(),
            new BrightnessAdjustmentTemplate(),
            new ContrastAdjustmentTemplate(),
            new GammaAdjustmentTemplate(),
            new SleeperTemplate()
        };

        public IEnumerator<IProcessingChainElementTemplate> GetEnumerator()
        {
            return AvailableProcessingChainElementsSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AvailableProcessingChainElementsSource.GetEnumerator();
        }
    }
}