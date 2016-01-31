using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class PixelsTests
    {
        [Test]
        public void ShouldReturnEnumerableOfSameLengthAsOneUsedToCreate()
        {
            // Given
            IEnumerable<Pixel> source = new Pixel[] { 1, 1, 1, 1 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(source.ToArray(), imageModel);

            // When
            var pixelsCount = image.ImagePixels.Count();

            // Then
            Assert.AreEqual(source.Count(), pixelsCount);
        }

        [Test]
        public void ShouldMaintainOrderOfSourceCollection()
        {
            // Given
            var source = new Pixel[] { 1, 2, 3, 4 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);

            // When
            var image = new Image(source, imageModel);

            // Then
            Assert.IsTrue(Enumerable.Range(0, source.Length).All(i => image.ImagePixels[i] == source[i]));
        }

        [Test]
        public void ShouldAllowToSetIndividualPixels()
        {
            // Given
            var source = new Pixel[] { 1, 2, 3, 4 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(source, imageModel);

            // When
            image.ImagePixels[2] = new Pixel(1);

            // Then
            Assert.AreEqual(new Pixel(1), image.ImagePixels[2]);
        }

        [Test]
        public void ShouldCorrectlyReturnIEnumerator()
        {
            // Given
            var source = new Pixel[] { 1, 2, 3, 4 };
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(source, imageModel);

            // When
            var enumerator = ((IEnumerable)image.ImagePixels).GetEnumerator();

            // Then
            Assert.IsNotNull(enumerator);
        }
    }
}