using Ulysses.Core.Models;

namespace Ulysses.ProcessingAlgorithms.Algorithms.PostProcessing
{
    public interface ITransformsImageModel
    {
        ImageModel GetTransformedImageModel(ImageModel imageModel);
    }
}