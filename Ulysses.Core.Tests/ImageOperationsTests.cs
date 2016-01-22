using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class ImageOperationsTests
    {
        [Test]
        public void ShouldCorrectlyAddTwoImages()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var firstImagePixels = Enumerable.Range(0, 1024 * 1024)
                                             .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                             .ToArray();
            var firstImage = new Image(firstImagePixels, imageModel);

            var secondImagePixels = Enumerable.Range(0, 1024 * 1024)
                                              .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                              .ToArray();
            var secondImage = new Image(secondImagePixels, imageModel);

            var sumOfPixels = firstImagePixels.Zip(secondImagePixels, (first, second) => first + second);

            // When
            var outputImage = firstImage + secondImage;

            // Then
            CollectionAssert.AreEqual(sumOfPixels, outputImage.ImagePixels);
        }
        
        [Test]
        public void ShouldNotAllowToAddTwoImagesOfDifferentImageModel()
        {
            // Given
            var random = new Random();

            var firstImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var firstImagePixels = Enumerable.Range(0, 1024 * 1024)
                                             .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                             .ToArray();
            var firstImage = new Image(firstImagePixels, firstImageModel);

            var secondImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp12);
            var secondImagePixels = Enumerable.Range(0, 1024 * 1024)
                                              .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                              .ToArray();
            var secondImage = new Image(secondImagePixels, secondImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() =>
            {
                var outputImage = firstImage + secondImage;
            });
        }

        [Test]
        public void ShouldCorrectlySubtractTwoImages()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var random = new Random();
            var firstImagePixels = Enumerable.Range(0, 1024 * 1024)
                                             .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                             .ToArray();
            var firstImage = new Image(firstImagePixels, imageModel);

            var secondImagePixels = Enumerable.Range(0, 1024 * 1024)
                                              .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                              .ToArray();
            var secondImage = new Image(secondImagePixels, imageModel);

            var sumOfPixels = firstImagePixels.Zip(secondImagePixels, (first, second) => first - second);

            // When
            var outputImage = firstImage - secondImage;

            // Then
            CollectionAssert.AreEqual(sumOfPixels, outputImage.ImagePixels);
        }
        
        [Test]
        public void ShouldNotAllowToSubtractTwoImagesOfDifferentImageModel()
        {
            // Given
            var random = new Random();

            var firstImageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);
            var firstImagePixels = Enumerable.Range(0, 1024 * 1024)
                                             .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                             .ToArray();
            var firstImage = new Image(firstImagePixels, firstImageModel);

            var secondImageModel = new ImageModel(1024, 512, ImageBitDepth.Bpp8);
            var secondImagePixels = Enumerable.Range(0, 1024 * 512)
                                              .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                                              .ToArray();
            var secondImage = new Image(secondImagePixels, secondImageModel);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() =>
            {
                var outputImage = firstImage - secondImage;
            });
        }
    }
}