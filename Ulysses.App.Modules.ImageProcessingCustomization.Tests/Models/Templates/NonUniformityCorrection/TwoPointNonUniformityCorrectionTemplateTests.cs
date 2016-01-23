using NUnit.Framework;
using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Models.Templates.NonUniformityCorrection
{
    [TestFixture]
    public class TwoPointNonUniformityCorrectionTemplateTests
    {
        [Test]
        public void ShouldNotAllowToChangeWhetherToUseNonUniformityModelOrNot()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => template.UseNonUniformityModel = false);
        }
    }
}
