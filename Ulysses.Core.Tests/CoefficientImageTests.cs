using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class CoefficientImageTests
    {
        [Test]
        public void ShouldCreateEmptyCoefficientImageProvidedOnlyImageModel()
        {
            // Given
            var imageModel = new ImageModel(25, 25, ImageBitDepth.Bpp8);
            var random = new Random();
            var coefficients = Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(i => random.NextDouble()).ToArray();

            // When
            var coefficientImage = new CoefficientImage(coefficients, imageModel);

            // Then
            CollectionAssert.AreEqual(coefficients, coefficientImage.Coefficients);
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
            Assert.Throws<ImageModelMismatchException>(() => new CoefficientImage(coefficients, imageModel));
        }
    }
}