using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class ProcessedImageOperationsTests
    {
        [Test]
        public void ShouldCorrectlyAddProcessedImageToImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var processedImage = new ProcessedImage(coefficients, imageModel);

            var sum = imagePixels.Zip(coefficients, (first, second) => (Pixel)(first + second));

            // When
            var output = image + processedImage;

            // Then
            CollectionAssert.AreEqual(sum, output.Values);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddedImageAndProcessedImageImageModelsDiffer()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var processedImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp12);
            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var processedImage = new ProcessedImage(coefficients, processedImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => { var output = processedImage + image; });
            Assert.Throws<ImageModelMismatchException>(() => { var output = image + processedImage; });
        }

        [Test]
        public void ShouldCorrectlySubtractProcessedImageToImage()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var processedImage = new ProcessedImage(coefficients, imageModel);

            var result = imagePixels.Zip(coefficients, (first, second) => (Pixel)(first - second));

            // When
            var output = image - processedImage;

            // Then
            CollectionAssert.AreEqual(result, output.Values);
        }

        [Test]
        public void ShouldThrowExceptionWhenSubtractingImageAndProcessedImageWithDifferentImageModels()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var imagePixels = Enumerable.Range(0, 1024 * 1024).Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue)).ToArray();
            var image = new Image(imagePixels, imageModel);

            var processedImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp12);
            var coefficients = Enumerable.Range(0, 1024 * 1024).Select(i => random.NextDouble() * ushort.MaxValue).ToArray();
            var processedImage = new ProcessedImage(coefficients, processedImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => { var output = processedImage - image; });
            Assert.Throws<ImageModelMismatchException>(() => { var output = image - processedImage; });
        }
    }
}