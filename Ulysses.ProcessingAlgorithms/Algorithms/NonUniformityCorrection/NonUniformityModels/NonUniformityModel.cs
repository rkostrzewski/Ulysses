using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class NonUniformityModel
    {
        public NonUniformityModel(ImageModel imageModel)
        {
            GainCoefficients = new CoefficientContainer(imageModel);
            OffsetCoefficients = new CoefficientContainer(imageModel);
        }

        public NonUniformityModel(double[,] gainCoefficients, double[,] offsetCoefficients, ImageBitDepth bitDepth)
        {
            GainCoefficients = new CoefficientContainer(gainCoefficients, bitDepth);
            OffsetCoefficients = new CoefficientContainer(offsetCoefficients, bitDepth);
        }

        public CoefficientContainer GainCoefficients { get; protected set; }
        public CoefficientContainer OffsetCoefficients { get; protected set; }
    }
}