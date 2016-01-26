using NUnit.Framework;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Tests.Factories
{
    [TestFixture]
    public class ImageProcessingAlgorithmsFactoryTests
    {
        [Test]
        public void ShouldCreateAlgorithmProvidedCorrectTemplate()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();
            var factory = new ImageProcessingAlgorithmsFactory();

            // When
            var algorithm = factory.CreateInstance(template);

            // Then
            Assert.IsInstanceOf<TwoPointNonUniformityCorrectionAlgorithm>(algorithm);
        }
    }
}
