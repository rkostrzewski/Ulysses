using System.Collections.Generic;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class NonUniformityModel
    {
        public NonUniformityModel(IEnumerable<double> gainCoefficients, IEnumerable<double> offsetCoefficients, ImageModel imageModel)
        {
            GainCoefficients = new ProcessedImage(gainCoefficients, imageModel);
            OffsetCoefficients = new ProcessedImage(offsetCoefficients, imageModel);
        }

        public ProcessedImage GainCoefficients { get; protected set; }
        public ProcessedImage OffsetCoefficients { get; protected set; }
    }
}