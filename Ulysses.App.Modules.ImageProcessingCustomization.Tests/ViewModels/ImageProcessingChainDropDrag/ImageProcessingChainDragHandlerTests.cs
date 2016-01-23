using System;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ImageProcessingChainDropDrag
{
    [TestFixture]
    public class ImageProcessingChainDragHandlerTests
    {
        [Test]
        [TestCase(typeof(ImageDisplayTemplate))]
        [TestCase(typeof(ImageProviderTemplate))]
        public void ShouldNotAllowToDragInputAndOutputItems(Type typeOfElement)
        {
            // Given
            var element = typeOfElement.GetConstructor(Type.EmptyTypes).Invoke(null);
            var dragHandler = new ImageProcessingChainDragHandler();
            var dragInfo = new TestDragInfo { SourceItems = new[] { element } };

            // When
            dragHandler.StartDrag(dragInfo);
            // Then
            Assert.IsNull(dragInfo.Data);
        }

        [Test]
        public void ShouldAllowToDragImageProcessingElements()
        {
            // Given
            var element = new TwoPointNonUniformityCorrectionTemplate();
            var dragHandler = new ImageProcessingChainDragHandler();
            var dragInfo = new TestDragInfo { SourceItems = new[] { element } };

            // When
            dragHandler.StartDrag(dragInfo);

            // Then
            Assert.IsNotNull(dragInfo.Data);
        }
    }
}