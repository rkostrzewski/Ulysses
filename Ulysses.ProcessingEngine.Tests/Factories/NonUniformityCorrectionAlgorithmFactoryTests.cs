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
    public class NonUniformityCorrectionAlgorithmFactoryTests
    {
        public void ShouldThrownExceptionWhenProvidedUnsupportedAlgorithm()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionAlgorithmTemplate
            {
                Algorithm = 0
            };

            var factory = new NonUniformityCorrectionAlgorithmFactory();

            // When
            // Then
            Assert.Throws<NotSupportedNonUniformityCorrectionAlgorithm>(() => factory.CreateAlgorithmImplementation(template));
        }

        public void ShouldCreateInstanceOfAlgorithmProvidedCorrectTemplate()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionAlgorithmTemplate
            {
                Algorithm = NonUniformityCorrectionAlgorithm.TwoPointNonUniformityAlgorithm,
                NonUniformityModel = new NonUniformityModel(new ImageModel(2, 2, ImageBitDepth.Bpp8))
            };

            var factory = new NonUniformityCorrectionAlgorithmFactory();

            // When
            var algorithm = factory.CreateAlgorithmImplementation(template);

            // Then
            Assert.IsInstanceOf<TwoPointNonUniformityCorrectionCorrectionAlgorithm>(algorithm);
        }
    }
}
