using Ulysses.NonUniformityCorrection.NonUniformityModels;

namespace Ulysses.NonUniformityCorrection.Factories.Templates
{
    public class TwoPointNonUniformityCorrectionAlgorithmTemplate : BaseNonUniformityCorrectionAlgorithmTemplate
    {
        public NonUniformityModel NonUniformityModel { get; set; }
    }
}