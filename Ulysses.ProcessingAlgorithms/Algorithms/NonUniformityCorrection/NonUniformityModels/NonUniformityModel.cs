using System.Collections.Generic;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class NonUniformityModel
    {
        public NonUniformityModel(IEnumerable<double> gainCoefficients, IEnumerable<double> offsetCoefficients, ImageModel imageModel)
        {
            GainCoefficients = new CoefficientImage(gainCoefficients, imageModel);
            OffsetCoefficients = new CoefficientImage(offsetCoefficients, imageModel);
        }

        public CoefficientImage GainCoefficients { get; protected set; }
        public CoefficientImage OffsetCoefficients { get; protected set; }
    }
}