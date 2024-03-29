using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Tests.Algorithms.NonUniformityCorrection
{
    [TestFixture]
    public class TwoPointNonUniformityCorrectionAlgorithmTests
    {
        [Test]
        public void ShouldReturnSameImageWhenProvidedDefaultNonUniformityModel()
        {
            // Given
            var nonUniformityModel = new DefaultNonUniformityModel(new ImageModel(2, 2, ImageBitDepth.Bpp8));
            var correctionAlgorithm = new TwoPointNonUniformityCorrectionAlgorithm(new TwoPointNonUniformityCorrectionTemplate { NonUniformityModel = nonUniformityModel });

            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(new ushort[] { 1, 2, 3, 4 }, imageModel);

            // When
            var correctedImage = correctionAlgorithm.ProcessImage(image);

            // Then
            Assert.AreEqual(correctedImage, image);
        }

        [Test]
        public void ShouldCorrectImageWhenProvidedNonUniformityModel()
        {
            // Given
            var nonUniformityModel = new NonUniformityModel(new[] { 1, 0, 0.5, 0.25 }, new[] { 5.0, 0, -4, 5 }, new ImageModel(2, 2, ImageBitDepth.Bpp8));
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var inputImage = new Image(new ushort[] { 5, 0, 2, 8 }, imageModel);
            var correctionAlgorithm = new TwoPointNonUniformityCorrectionAlgorithm(new TwoPointNonUniformityCorrectionTemplate { NonUniformityModel = nonUniformityModel });

            // When
            var correctedImage = correctionAlgorithm.ProcessImage(inputImage);

            // Then
            var expectedImage = new Image(new ushort[] { 10, 0, 0, 7 }, imageModel);
            Assert.AreEqual(expectedImage, correctedImage);
        }

        [Test]
        public void ShouldModifyInputImageForNonTrivialNonUniformityModel()
        {
            // Given
            var nonUniformityModel = new NonUniformityModel(new[] { 1, 0, 0.5, 0.25 }, new[] { 5.0, 0, -4, 5 }, new ImageModel(2, 2, ImageBitDepth.Bpp8));
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var inputImage = new Image(new ushort[] { 5, 0, 2, 8 }, imageModel);
            var correctionAlgorithm = new TwoPointNonUniformityCorrectionAlgorithm(new TwoPointNonUniformityCorrectionTemplate { NonUniformityModel = nonUniformityModel });

            // When
            var correctedImage = correctionAlgorithm.ProcessImage(inputImage);

            // Then
            Assert.AreNotEqual(inputImage, correctedImage);
        }

        [Test]
        public void ShouldThrowErrorWhenTryingToCorrectImageOfDifferentModelThanNonUniformityImageModel()
        {
            // Given
            var imageModel = new ImageModel(4, 2, ImageBitDepth.Bpp12);
            var inputImage = new Image(new ushort[] { 5, 0, 2, 8, 2, 8, 2, 8 }, imageModel);

            var nonUniformityModel = new NonUniformityModel(new[] { 1, 0, 0.5, 0.25 }, new[] { 5.0, 0, -4, 5 }, new ImageModel(2, 2, ImageBitDepth.Bpp8));

            var correctionAlgorithm = new TwoPointNonUniformityCorrectionAlgorithm(new TwoPointNonUniformityCorrectionTemplate { NonUniformityModel = nonUniformityModel });

            // When
            // Then
            Assert.Throws<ImageModelMismatchException>(() => { correctionAlgorithm.ProcessImage(inputImage); });
        }
    }
}