using NUnit.Framework;
using Ulysses.Core.Models;

namespace Ulysses.Core.Tests
{
    [TestFixture]
    public class ImageModelTests
    {
        [Test]
        public void ShouldCorrectlyMaintainParametersProvidedWhenCreated()
        {
            // Given
            const int width = 256;
            const int height = 512;
            const ImageBitDepth imageBitDepth = ImageBitDepth.Bpp12;

            // When
            var imageModel = new ImageModel(width, height, imageBitDepth);

            // Then
            Assert.AreEqual(width, imageModel.Width);
            Assert.AreEqual(height, imageModel.Height);
            Assert.AreEqual(imageBitDepth, imageModel.ImageBitDepth);
        }

        [Test]
        [TestCaseSource(typeof(ImageModelTestCases), nameof(ImageModelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImageModels(ImageModel first, ImageModel second, bool expected)
        {
            // Given
            // When

            var actual = first == second;

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageModelTestCases), nameof(ImageModelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImageModelsUsingEqualsCall(ImageModel first, ImageModel second, bool expected)
        {
            // Given
            // When

            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageModelTestCases), nameof(ImageModelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkEqualityOfImageModelAndObject(ImageModel first, object second, bool expected)
        {
            // Given
            // When

            var actual = first.Equals(second);

            // Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(ImageModelTestCases), nameof(ImageModelTestCases.EqualityTestCases))]
        public void ShouldCorrectlyMarkInequalityOfImageModels(ImageModel first, ImageModel second, bool notExpected)
        {
            // Given
            // When

            var actual = first != second;

            // Then
            Assert.AreNotEqual(notExpected, actual);
        }

        [Test]
        public void ShouldCorrectlyGenerateHashCode()
        {
            // Given
            const int width = 256;
            const int height = 512;
            const ImageBitDepth imageBitDepth = ImageBitDepth.Bpp12;
            var imageModel = new ImageModel(width, height, imageBitDepth);

            int expected;

            unchecked
            {
                expected = width;
                expected = (expected * 397) ^ height;
                expected = (expected * 397) ^ (int)imageBitDepth;
            }

            // When
            var actual = imageModel.GetHashCode();

            // Then
            Assert.AreEqual(expected, actual);
        }
    }
}