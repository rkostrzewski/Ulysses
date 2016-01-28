using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class ProcessedImageTests
    {
        [Test]
        public void ShouldCreateEmptyProcessedImageProvidedOnlyImageModel()
        {
            // Given
            var imageModel = new ImageModel(25, 25, ImageBitDepth.Bpp8);
            var random = new Random();
            var coefficients = Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(i => random.NextDouble()).ToArray();

            // When
            var processedImage = new ProcessedImage(coefficients, imageModel);

            // Then
            CollectionAssert.AreEqual(coefficients, processedImage.Values);
        }

        [Test]
        public void ShouldThrowExceptionWhenCoefficientsLengthDoesNotMatchDimensionsOfImageModel()
        {
            // Given
            var imageModel = new ImageModel(25, 25, ImageBitDepth.Bpp8);
            var random = new Random();
            var coefficients = Enumerable.Range(0, 25).Select(i => random.NextDouble()).ToArray();

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => new ProcessedImage(coefficients, imageModel));
        }
    }
}