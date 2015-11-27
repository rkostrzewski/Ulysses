using Ulysses.Core.Models;

namespace Ulysses.Core
{
    /// <summary>
    ///     Interface for Non Uniformity Correction Algorithms.
    /// </summary>
    public interface IImageProcessingAlgorithm
    {
        Image ProcessImage(Image inputImage);
    }
}