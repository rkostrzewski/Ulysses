using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class CoefficientImageOperationsTests
    {
        [Test]
        public void ShouldCorrectlyAddImageToCoefficientImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, imageModel);

            var sum = imagePixels.Zip(coefficients, (first, second) => (Pixel)(first + second));

            // When
            var output = coefficientImage + image;

            // Then
            CollectionAssert.AreEqual(sum, output.ImagePixels);
        }

        [Test]
        public void ShouldCorrectlyAddCoefficientImageToImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, imageModel);

            var sum = imagePixels.Zip(coefficients, (first, second) => (Pixel)(first + second));

            // When
            var output = image + coefficientImage;

            // Then
            CollectionAssert.AreEqual(sum, output.ImagePixels);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddedImageAndCoefficientImageImageModelsDiffer()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficientImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp12);
            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, coefficientImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => { var output = coefficientImage + image; });
            Assert.Throws<ImageModelMismatchException>(() => { var output = image + coefficientImage; });
        }

        [Test]
        public void ShouldCorrectlySubtractImageToCoefficientImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, imageModel);

            var result = coefficients.Zip(imagePixels, (first, second) => (Pixel)(first - second));

            // When
            var output = coefficientImage - image;

            // Then
            CollectionAssert.AreEqual(result, output.ImagePixels);
        }

        [Test]
        public void ShouldCorrectlySubtractCoefficientImageToImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, imageModel);

            var result = imagePixels.Zip(coefficients, (first, second) => (Pixel)(first - second));

            // When
            var output = image - coefficientImage;

            // Then
            CollectionAssert.AreEqual(result, output.ImagePixels);
        }

        [Test]
        public void ShouldThrowExceptionWhenSubtractingImageAndCoefficientImageWithDifferentImageModels()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficientImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp12);
            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var coefficientImage = new CoefficientImage(coefficients, coefficientImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => { var output = coefficientImage - image; });
            Assert.Throws<ImageModelMismatchException>(() => { var output = image - coefficientImage; });
        }
    }
}