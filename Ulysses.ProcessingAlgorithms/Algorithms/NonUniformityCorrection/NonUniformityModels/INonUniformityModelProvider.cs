using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels
{
    public interface INonUniformityModelProvider
    {
        NonUniformityModel GetNonUniformityModel(string sourceFilePath, ImageModel imageModel);
    }
}