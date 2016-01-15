using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.ProcessingAlgorithms.Factories.NonUniformityCorrection.Templates
{
    public class TwoPointNonUniformityCorrectionAlgorithmTemplate : BaseNonUniformityCorrectionAlgorithmTemplate
    {
        public NonUniformityModel NonUniformityModel { get; set; }
    }
}