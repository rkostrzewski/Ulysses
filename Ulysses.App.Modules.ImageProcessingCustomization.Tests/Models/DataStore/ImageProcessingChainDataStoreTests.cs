using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageAcquisition;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Models.DataStore
{
    [TestFixture]
    public class ImageProcessingChainDataStoreTests
    {
        [Test]
        public void ShouldContainImageProviderAndImageDisplayTemplatesWhenCreated()
        {
            // Given
            // When
            var dataStore = new ImageProcessingChainDataStore();

            // Then
            Assert.AreEqual(2, dataStore.ImageProcessingChainElements.Count);
            Assert.IsTrue(dataStore.ImageProcessingChainElements.Any(i => i.GetType() == typeof(ImageProviderTemplate)));
            Assert.IsTrue(dataStore.ImageProcessingChainElements.Any(i => i.GetType() == typeof(ImageDisplayTemplate)));
        }

        [Test]
        public void ShouldGenerateUniqueIdsForItemsWithoutIdAddedToStore()
        {
            // Given
            var dataStore = new ImageProcessingChainDataStore();
            var template = new TwoPointNonUniformityCorrectionTemplate { Id = null };

            // When
            dataStore.ImageProcessingChainElements.Add(template);

            // Then
            Assert.IsNotNull(template.Id);
            CollectionAssert.AllItemsAreUnique(dataStore.ImageProcessingChainElements.Select(i => i.Id));

        }

        [Test]
        public void ShouldNotGenerateIdsForItemsWithIdNotPresentInCollectionAddedToStore()
        {
            // Given
            var dataStore = new ImageProcessingChainDataStore();
            var templateId = Guid.NewGuid().ToString();
            var template = new TwoPointNonUniformityCorrectionTemplate { Id = templateId };

            // When
            dataStore.ImageProcessingChainElements.Add(template);

            // Then
            Assert.AreEqual(templateId, template.Id);
            CollectionAssert.AllItemsAreUnique(dataStore.ImageProcessingChainElements.Select(i => i.Id));
        }

        [Test]
        public void ShouldGenerateIdsForItemsWithIdThatIsAlreadyPresentInCollectionAddedToStore()
        {
            // Given
            var dataStore = new ImageProcessingChainDataStore();
            var templateId = dataStore.ImageProcessingChainElements.First().Id;
            var template = new TwoPointNonUniformityCorrectionTemplate { Id = templateId };

            // When
            dataStore.ImageProcessingChainElements.Add(template);

            // Then
            Assert.AreNotEqual(templateId, template.Id);
            CollectionAssert.AllItemsAreUnique(dataStore.ImageProcessingChainElements.Select(i => i.Id));
        }
    }
}
