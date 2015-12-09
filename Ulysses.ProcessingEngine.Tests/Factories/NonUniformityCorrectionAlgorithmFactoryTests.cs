using NUnit.Framework;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.NUC.Algorithms;
using Ulysses.NUC.Exceptions;
using Ulysses.NUC.Factories;
using Ulysses.NUC.Factories.Templates;
using Ulysses.NUC.NonUniformityModels;

namespace Ulysses.ProcessingEngine.Tests.Factories
{
    [TestFixture]
    public class NUCAlgorithmFactoryTests
    {
        [Test]
        public void ShouldThrownExceptionWhenProvidedUnsupportedAlgorithm()
        {
            // Given
            var template = new TwoPointNUCAlgorithmTemplate
            {
                Algorithm = 0
            };

            var factory = new NUCAlgorithmFactory();

            // When
            // Then
            Assert.Throws<NotSupportedNUCAlgorithm>(() => factory.CreateAlgorithmImplementation(template));
        }

        [Test]
        public void ShouldCreateInstanceOfAlgorithmProvidedCorrectTemplate()
        {
            // Given
            var template = new TwoPointNUCAlgorithmTemplate
            {
                Algorithm = NUCAlgorithm.TwoPointNonUniformityAlgorithm,
                NonUniformityModel = new NonUniformityModel(new ImageModel(2, 2, ImageBitDepth.Bpp8))
            };

            var factory = new NUCAlgorithmFactory();

            // When
            var algorithm = factory.CreateAlgorithmImplementation(template);

            // Then
            Assert.IsInstanceOf<TwoPointNUCAlgorithm>(algorithm);
        }
    }
}
