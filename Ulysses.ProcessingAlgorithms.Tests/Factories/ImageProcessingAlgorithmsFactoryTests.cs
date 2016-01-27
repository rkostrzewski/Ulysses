using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Algorithms.DummyAlgorithms;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;

namespace Ulysses.ProcessingAlgorithms.Tests.Factories
{
    [TestFixture]
    public class ImageProcessingAlgorithmsFactoryTests
    {
        [Test]
        public void ShouldCreateAlgorithmProvidedCorrectTemplate()
        {
            // Given
            var template = new SleeperTemplate();
            var imageModel = new ImageModel(0, 0, ImageBitDepth.Bpp8);
            var nonUniformityCorrectionModelProvider = new NonUniformityModelProvider();
            var factory = new ImageProcessingAlgorithmsFactory(nonUniformityCorrectionModelProvider);

            // When
            var algorithm = factory.CreateInstance(template, imageModel);

            // Then
            Assert.IsInstanceOf<Sleeper>(algorithm);
        }
    }
}