using NUnit.Framework;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.NUC.Algorithms;
using Ulysses.NUC.Factories;
using Ulysses.NUC.Factories.Templates;
using Ulysses.NUC.NonUniformityModels;
using Ulysses.ProcessingEngine.ImageProcessing;

namespace Ulysses.ProcessingEngine.Tests.NonUniformityCorrectionChaining
{
    [TestFixture]
    public class ImageProcessingChainTests
    {
        [Test]
        public void ShouldReturnInputWhenChainIsEmpty()
        {
            // Given
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(new ushort[] { 1, 2, 3, 4 }, imageModel);

            var chain = new ImageProcessingChainBuilder().Build();

            // When
            var outputImage = chain.ProcessImage(image);

            // Then
            Assert.AreEqual(image, outputImage);
        }

        [Test]
        public void ShouldReturnCorrectImageWhenChainConsistsFromOneMethod()
        {
            // Given
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(new ushort[] { 5, 0, 2, 8 }, imageModel);

            var nonUniformityModel =
                new NonUniformityModel(new[,] { { 1, 0 }, { 0.5, 0.25 } }, new[,] { { 5.0, 0 }, { -4, 5 } }, ImageBitDepth.Bpp8);
            var algorithm = new NonUniformityAlgorithmFactory().Create(new TwoPointNonUniformityTemplate
            {
                Algorithm = Algorithm.TwoPointNonUniformityAlgorithm,
                NonUniformityModel = nonUniformityModel
            });

            var chain = new ImageProcessingChainBuilder()
                .AddStepToChain(algorithm)
                .Build();

            // When
            var outputImage = chain.ProcessImage(image);

            // Then
            var expectedImage = new Image(new ushort[] { 10, 0, 0, 7 }, imageModel);
            Assert.AreEqual(expectedImage, outputImage);
        }

        [Test]
        public void ShouldReturnCorrectImageWhenChainConsistsOfTwoMethods()
        {
            // Given
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var image = new Image(new ushort[] { 5, 5, 5, 5 }, imageModel);

            var nonUniformityModel =
                new NonUniformityModel(new[,] { { 1.0, 0 }, { 1.0, 0 } } , new[,] { { 0, 6.0 }, { 0, 6.0 } }, ImageBitDepth.Bpp8);
            var algorithm = new NonUniformityAlgorithmFactory().Create(new TwoPointNonUniformityTemplate
            {
                Algorithm = Algorithm.TwoPointNonUniformityAlgorithm,
                NonUniformityModel = nonUniformityModel
            });

            var chainBuilder = new ImageProcessingChainBuilder()
                                .AddStepToChain(algorithm);

            nonUniformityModel = new NonUniformityModel(new[,] { { 0.2, 1.0 }, { 0.2, 1.0 } }, new[,] { { 0, -5.0 }, { 0, -5.0 } }, ImageBitDepth.Bpp8);
            algorithm = new NonUniformityAlgorithmFactory().Create(new TwoPointNonUniformityTemplate
            {
                Algorithm = Algorithm.TwoPointNonUniformityAlgorithm,
                NonUniformityModel = nonUniformityModel
            });

            var chain = chainBuilder
                .AddStepToChain(algorithm)
                .Build();

            // When
            var outputImage = chain.ProcessImage(image);

            // Then
            var expectedImage = new Image(new ushort[] { 1, 1, 1, 1 }, imageModel);
            Assert.AreEqual(expectedImage, outputImage);
        }
    }
}
