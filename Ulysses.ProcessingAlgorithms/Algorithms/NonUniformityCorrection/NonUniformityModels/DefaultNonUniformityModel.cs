using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class DefaultNonUniformityModel : NonUniformityModel
    {
        public DefaultNonUniformityModel(ImageModel imageModel) : base(imageModel)
        {
            GainCoefficients = new CoefficientContainer(imageModel);
            OffsetCoefficients = new CoefficientContainer(imageModel);

            for (var x = 0; x < imageModel.Width; x++)
            {
                for (var y = 0; y < imageModel.Height; y++)
                {
                    GainCoefficients.Values[x, y] = 1.0;
                    OffsetCoefficients.Values[x, y] = 0.0;
                }
            }
        }
    }
}