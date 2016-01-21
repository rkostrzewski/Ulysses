using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class PixelsTests
    {
        [Test]
        public void ShouldCreatePixelMatrixFromPixelEnumerable()
        {
            // Given
            IEnumerable<Pixel> pixelsEnumerable = new Pixel[] { 1, 1, 1, 1 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);

            // When
            var pixels = new Pixels(pixelsEnumerable, imageModel);

            // Then
            CollectionAssert.AreEqual(pixelsEnumerable, pixels);
        }

        [Test]
        public void ShouldShouldThrowImageModelMismatchExceptionWhenImageModelDimensionsDifferFromProvidedPixelsLength()
        {
            // Given
            IEnumerable<Pixel> pixelsEnumerable = new Pixel[] { 1, 1 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => new Pixels(pixelsEnumerable, imageModel));
        }

        [Test]
        public void ShouldReturnEnumerableOfSameLenghtAsOneUsedToCreate()
        {
            // Given
            IEnumerable<Pixel> pixelsEnumerable = new Pixel[] { 1, 1, 1, 1 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var pixels = new Pixels(pixelsEnumerable, imageModel);

            // When
            var pixelsCount = pixels.Count();

            // Then
            Assert.AreEqual(pixelsEnumerable.Count(), pixelsCount);
        }

        [Test]
        public void ShouldMaintainOrderOfSourceCollection()
        {
            // Given
            var source = new Pixel[] { 1, 2, 3, 4 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);

            // When
            var pixels = new Pixels(source, imageModel);

            // Then
            Assert.IsTrue(Enumerable.Range(0, source.Count()).All(i => pixels[i] == source[i]));
        }

        [Test]
        public void ShouldAllowToSetIndividualPixels()
        {
            // Given
            var source = new Pixel[] { 1, 2, 3, 4 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var pixels = new Pixels(source, imageModel);

            // When
            pixels[2] = new Pixel(1);

            // Then
            Assert.AreEqual(new Pixel(1), pixels[2]);
        }
    }
}