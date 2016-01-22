using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.Core.Tests.TestCases;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void ShouldCreateEmptyImageProvidedOnlyImageModel()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp8);

            // When
            var image = new Image(imageModel);

            // Then
            Assert.IsTrue(image.ImagePixels.All(p => p == new Pixel()));
        }

        [Test]
        public void ShouldRetainImageModelWhenCreatedFromImageModelOnly()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp8);

            // When
            var image = new Image(imageModel);

            // Then
            Assert.AreEqual(imageModel, image.ImageModel);
        }

        [Test]
        public void ShouldCorrectlyCreateImageFromByteArrayOfPixelValues()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp8);
            var pixels = new byte[256 * 256];
            new Random().NextBytes(pixels);

            // When
            var image = new Image(pixels, imageModel);

            // Then
            Assert.IsTrue(Enumerable.Range(0, pixels.Length).All(i => image.ImagePixels[i] == pixels[i]));
        }

        [Test]
        public void ShouldCorrectlyCreateImageFromUshortArrayOfPixelValues()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp8);
            var random = new Random();
            var pixels = Enumerable.Range(0, 256 * 256)
                .Select(i => (ushort)random.Next(ushort.MinValue, ushort.MaxValue))
                .ToArray();
            

            // When
            var image = new Image(pixels, imageModel);

            // Then
            Assert.IsTrue(Enumerable.Range(0, pixels.Length).All(i => image.ImagePixels[i] == pixels[i]));
        }

        [Test]
        public void ShouldCorrectlyCreateImageFromPixelArray()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp8);
            var random = new Random();
            var pixels = Enumerable.Range(0, 256 * 256)
                .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                .ToArray();


            // When
            var image = new Image(pixels, imageModel);

            // Then
            Assert.IsTrue(Enumerable.Range(0, pixels.Length).All(i => image.ImagePixels[i] == pixels[i]));
        }

        [Test]
        public void ShouldRetainImageModelWhenCreatedFromPixelsAndImageModel()
        {
            // Given
            var imageModel = new ImageModel(256, 256, ImageBitDepth.Bpp12);
            var random = new Random();
            var pixels = Enumerable.Range(0, 256 * 256)
                .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                .ToArray();

            // When
            var image = new Image(pixels, imageModel);

            // Then
            Assert.AreEqual(imageModel, image.ImageModel);
        }

        [Test]
        public void ShouldShouldThrowImageModelMismatchExceptionWhenImageModelDimensionsDifferFromProvidedPixelsLength()
        {
            // Given
            var imageModel = new ImageModel(256, 128, ImageBitDepth.Bpp12);
            var random = new Random();
            var pixels = Enumerable.Range(0, 256 * 256)
                .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                .ToArray();

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => new Image(pixels, imageModel));
        }

        [Test]
        [TestCaseSource(typeof(ImageTestCases), nameof(ImageTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImagesUsingEqualityOperator(Image first, Image second, bool expected)
        {
            // Given
            // When
            var actual = first == second;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageTestCases), nameof(ImageTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImagesUsingEqualsCall(Image first, Image second, bool expected)
        {
            // Given
            // When
            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageTestCases), nameof(ImageTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImageAndObjectUsingEqualsCall(Image first, object second, bool expected)
        {
            // Given
            // When
            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageTestCases), nameof(ImageTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkInequalityOfImagesUsingInequalityOperator(Image first, Image second, bool notExpected)
        {
            // Given
            // When
            var actual = first != second;

            // Then
            Assert.AreNotEqual(notExpected, actual);
        }

        [Test]
        public void ShouldCorrectlyReturnHashCode()
        {
            // Given
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp12);
            var random = new Random();
            var pixels = Enumerable.Range(0, 2 * 2)
                .Select(i => (Pixel)random.Next(ushort.MinValue, ushort.MaxValue))
                .ToArray();

            var image = new Image(pixels, imageModel);
            var expectedHashCode = (image.ImagePixels.GetHashCode() * 397) ^ imageModel.GetHashCode();


            // When
            var actualHashCode = image.GetHashCode();

            // Then
            Assert.AreEqual(expectedHashCode, actualHashCode);
        }
    }
}