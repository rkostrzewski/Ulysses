﻿using System;
using Moq;
using NUnit.Framework;
using Prism.Events;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ProcessingChainDropDrag
{
    [TestFixture]
    public class ProcessingChainDragHandlerTests
    {
        private IEventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new Mock<IEventAggregator>().Object;
        }

        [Test]
        [TestCase(typeof (ImageDisplayTemplateTemplate))]
        [TestCase(typeof (ImageProviderTemplate))]
        public void ShouldNotAllowToDragInputAndOutputItems(Type typeOfElement)
        {
            // Given
            var element = typeOfElement.GetConstructor(Type.EmptyTypes).Invoke(null);
            var dragHandler = new ProcessingChainDragHandler();
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
            var dragHandler = new ProcessingChainDragHandler();
            var dragInfo = new TestDragInfo { SourceItems = new[] { element } };

            // When
            dragHandler.StartDrag(dragInfo);

            // Then
            Assert.IsNotNull(dragInfo.Data);
        }
    }
}