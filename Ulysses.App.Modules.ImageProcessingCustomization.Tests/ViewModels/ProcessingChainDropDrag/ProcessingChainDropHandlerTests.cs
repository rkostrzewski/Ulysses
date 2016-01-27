using System;
using System.Collections.Generic;
using System.Windows;
using Moq;
using NUnit.Framework;
using Prism.Events;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.Core.Templates;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.ViewModels.ProcessingChainDropDrag
{
    [TestFixture]
    public class ProcessingChainDropHandlerTests
    {
        private IEventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new Mock<IEventAggregator>().Object;
        }

        [Test]
        [TestCase(typeof (ImageProviderTemplate), false)]
        [TestCase(typeof (TwoPointNonUniformityCorrectionTemplate), true)]
        public void ShouldNotAllowToDragOverItemsOtherThanImageProcessingAlgorithmTemplates(Type elementType, bool shouldAllowToDragOver)
        {
            // Given
            var dropInfo = new TestDropInfo
            {
                Data = elementType.GetConstructor(Type.EmptyTypes)?.Invoke(null),
                TargetCollection = new object[3],
                DragInfo = new TestDragInfo { SourceCollection = new object[3] },
                InsertIndex = 1
            };

            var dropHandler = new ProcessingChainDropHandler();

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
            var dropInfo = new TestDropInfo { Data = new TwoPointNonUniformityCorrectionTemplate(), TargetCollection = new object[3], InsertIndex = index };

            var dropHandler = new ProcessingChainDropHandler();

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
            var dataSource = new ProcessingChainDataStore();
            var targetCollection = dataSource.ProcessingChainTemplate;
            var sourceCollection = new List<IProcessingChainElementTemplate> { element };

            sourceCollection.Insert(index, element);

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo { SourceCollection = sourceCollection },
                InsertIndex = index
            };

            var dropHandler = new ProcessingChainDropHandler();

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
            var dataSource = new ProcessingChainDataStore();
            dataSource.ProcessingChainTemplate.Insert(index, new TwoPointNonUniformityCorrectionTemplate());
            dataSource.ProcessingChainTemplate.Insert(index + 1, element);

            var sourceCollection = dataSource.ProcessingChainTemplate;
            var targetCollection = sourceCollection;

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo { SourceCollection = sourceCollection },
                InsertIndex = index
            };

            var dropHandler = new ProcessingChainDropHandler();

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
            var dataSource = new ProcessingChainDataStore();

            var sourceCollection = new List<object> { element };
            var targetCollection = dataSource.ProcessingChainTemplate;

            var dropInfo = new TestDropInfo
            {
                Data = element,
                TargetCollection = targetCollection,
                DragInfo = new TestDragInfo { SourceCollection = sourceCollection },
                InsertIndex = index
            };

            var dropHandler = new ProcessingChainDropHandler();

            // When
            // Then
            Assert.Throws<ArgumentException>(() => dropHandler.Drop(dropInfo));
        }
    }
}