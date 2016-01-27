using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.ImageProcessingCustomization.Tests.Helpers;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Regions.ViewLocators
{
    [TestFixture]
    public class ImageProcessingChainElementViewLocatorTests
    {
        [Test]
        public void ShouldReturnDefaultViewTypeWhenViewWasNotFoundForTemplate()
        {
            // Given
            var viewLocator = new ImageProcessingChainElementViewLocator();

            // When
            var viewType = viewLocator.GetViewType(new TestChainElementTemplateTemplateTemplateTemplate());

            // Then
            Assert.AreEqual(typeof (EmptyChainElementCustomizationView), viewType);
        }

        [Test]
        public void ShouldReturnViewTypeWhenViewWasFoundForTemplate()
        {
            // Given
            var viewLocator = new ImageProcessingChainElementViewLocator();

            // When
            var viewType = viewLocator.GetViewType(new TwoPointNonUniformityCorrectionTemplate());

            // Then
            Assert.AreEqual(typeof (TwoPointNonUniformityCorrectionCustomizationView), viewType);
        }
    }
}