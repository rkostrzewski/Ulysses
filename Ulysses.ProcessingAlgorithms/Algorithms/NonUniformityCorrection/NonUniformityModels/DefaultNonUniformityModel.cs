using System.Linq;
using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public class DefaultNonUniformityModel : NonUniformityModel
    {
        public DefaultNonUniformityModel(ImageModel imageModel)
            : base(
                Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(i => 1.0d).ToArray(),
                Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(i => 0.0d).ToArray(),
                imageModel)
        {
        }
    }
}