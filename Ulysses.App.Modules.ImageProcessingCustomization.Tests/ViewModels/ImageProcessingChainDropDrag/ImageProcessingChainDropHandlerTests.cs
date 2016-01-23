using System;
using System.Collections.Generic;
using System.Windows;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ImageProcessingChainDropDrag
{
    [TestFixture]
    public class ImageProcessingChainDropHandlerTests
    {
        [Test]
        [TestCase(typeof(ImageProviderTemplate), false)]
        [TestCase(typeof(TwoPointNonUniformityCorrectionTemplate), true)]
        public void ShouldNotAllowToDragOverItemsOtherThanImageProcessingAlgorithmTemplates(Type elementType, bool shouldAllowToDragOver)
        {
            // Given
            var dropInfo = new TestDropInfo
            {
                Data = elementType.GetConstructor(Type.EmptyTypes)?.Invoke(null),
                TargetCollection = new object[3],
                DragInfo = new TestDragInfo
                {
                    SourceCollection = new object[3]
                },
                InsertIndex = 1
            };

            var dropHandler = new ImageProcessingChainDropHandler();

            // When
            dropHandler.DragOver(dropInfo);

            // Then
            Assert.AreEqual(shouldAllowToDragOver, dropInfo.Effects != DragDropEffects.None);
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        public void ShouldNotAllowToDragOverItemsAtStartOrEndOfCollection(int index)
        {
            // Given
            var dropInfo = new TestDropInfo
            {
                Data = new TwoPointNonUniformityCorrectionTemplate(),
                TargetCollection = new object[3],
                InsertIndex = index
            };

            var dropHandler = new ImageProcessingChainDropHandler();

            // When
            dropHandler.DragOver(dropInfo);

            // Then
            Assert.AreEqual(DragDropEffects.None, dropInfo.Effects);
        }

        [Test]
        public void ShouldCreateNewObjectWhenDroppedFromDifferentCollectionThanSourceCollection()
        {
            // Given
            const int index = 1;
            var element = new TwoPointNonUniformityCorrectionTemplate();
            var dataSource = new ImageProcessingChainDataStore();
            var targetCollection = dataSource.ImageProcessingChainElements;
            var sourceCollection = new List<IImageProcessingChainElement>
            {
                element
            };

            sourceCollection.Insert(index, element);

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo
                {
                    SourceCollection = sourceCollection
                },
                InsertIndex = index
            };

            var dropHandler = new ImageProcessingChainDropHandler();

            // When
            dropHandler.Drop(dropInfo);
            var targetCollectionElement = targetCollection[index];

            // Then
            Assert.IsNotNull(targetCollectionElement);
            Assert.AreEqual(index, targetCollection.IndexOf(targetCollectionElement));
            Assert.IsFalse(ReferenceEquals(element, targetCollectionElement));
        }

        [Test]
        public void ShouldNotCreateNewObjectWhenDroppedFromDifferentCollectionThanSourceCollection()
        {
            // Given
            const int index = 1;
            var element = new TwoPointNonUniformityCorrectionTemplate();
            var dataSource = new ImageProcessingChainDataStore();
            dataSource.ImageProcessingChainElements.Insert(index, new TwoPointNonUniformityCorrectionTemplate());
            dataSource.ImageProcessingChainElements.Insert(index + 1, element);

            var sourceCollection = dataSource.ImageProcessingChainElements;
            var targetCollection = sourceCollection;

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo
                {
                    SourceCollection = sourceCollection
                },
                InsertIndex = index
            };

            var dropHandler = new ImageProcessingChainDropHandler();

            // When
            dropHandler.Drop(dropInfo);
            var targetCollectionElement = targetCollection[index];

            // Then
            Assert.IsNotNull(targetCollectionElement);
            Assert.AreEqual(index, targetCollection.IndexOf(targetCollectionElement));
            Assert.IsTrue(ReferenceEquals(element, targetCollectionElement));
        }

        [Test]
        public void ShouldThrowExceptionWhenObjectDroppedFromOtherCollectionDoesNotContainParameterlessConstructor()
        {
            // Given
            const int index = 1;
            var element = new ParameterlessConstructorTestObject(1);
            var dataSource = new ImageProcessingChainDataStore();
            
            var sourceCollection = new List<object> { element };
            var targetCollection = dataSource.ImageProcessingChainElements;

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo
                {
                    SourceCollection = sourceCollection
                },
                InsertIndex = index
            };

            var dropHandler = new ImageProcessingChainDropHandler();

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => dropHandler.Drop(dropInfo));
        }
    }
}