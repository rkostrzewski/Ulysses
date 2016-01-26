using System;
using System.Linq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Models.DataStore
{
    [TestFixture]
    public class ProcessingChainTemplateTests
    {
        [Test]
        public void ShouldGenerateUniqueIdsForItemsWithoutIdAddedToChain()
        {
            // Given
            var processingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };
            var template = new TwoPointNonUniformityCorrectionTemplate { Id = null };

            // When
            processingChainTemplate.Insert(1, template);

            // Then
            Assert.IsNotNull(template.Id);
            CollectionAssert.AllItemsAreUnique(processingChainTemplate.Select(i => i.Id));
        }

        [Test]
        public void ShouldNotGenerateIdsForItemsWithIdNotPresentInCollectionAddedToChain()
        {
            // Given
            var processingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };

            var templateId = Guid.NewGuid().ToString();
            var template = new TwoPointNonUniformityCorrectionTemplate { Id = templateId };

            // When
            processingChainTemplate.Insert(1, template);

            // Then
            Assert.AreEqual(templateId, template.Id);
            CollectionAssert.AllItemsAreUnique(processingChainTemplate.Select(i => i.Id));
        }

        [Test]
        public void ShouldNotGenerateNewIdWhenItemIsMoved()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();

            var processingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };
            processingChainTemplate.Insert(1, new TwoPointNonUniformityCorrectionTemplate());
            processingChainTemplate.Insert(2, template);

            var templateId = processingChainTemplate.Single(i => i == template).Id;

            // When
            processingChainTemplate.RemoveAt(2);
            processingChainTemplate.Insert(1, template);

            // Then
            Assert.AreEqual(templateId, template.Id);
            CollectionAssert.AllItemsAreUnique(processingChainTemplate.Select(i => i.Id));
        }

        [Test]
        public void ShouldGenerateNewIdWhenItemWithSameIdIsAlreadyPresentInChain()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();

            var processingChainTemplate = new ProcessingChainTemplate { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };
            processingChainTemplate.Insert(1, new TwoPointNonUniformityCorrectionTemplate());

            var templateId = processingChainTemplate.ElementAt(1).Id;

            // When
            processingChainTemplate.Insert(1, template);

            // Then
            Assert.AreNotEqual(templateId, template.Id);
            CollectionAssert.AllItemsAreUnique(processingChainTemplate.Select(i => i.Id));
        }

        [Test]
        public void ShouldNotAllowToAddElementOtherThanIImageProviderTemplateAtStart()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();
            var processingChainTemplate = new ProcessingChainTemplate();

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => processingChainTemplate.Insert(0, template));
        }

        [Test]
        public void ShouldNotAllowToAddSecondIImageProviderTemplateToChain()
        {
            // Given
            var template = new ImageProviderTemplate();
            var processingChainTemplate = new ProcessingChainTemplate() { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => processingChainTemplate.Insert(1, template));
        }

        [Test]
        public void ShouldNotAllowToAddElementOtherThanIImageDisplayTemplateAtEnd()
        {
            // Given
            var template = new TwoPointNonUniformityCorrectionTemplate();
            var processingChainTemplate = new ProcessingChainTemplate() { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => processingChainTemplate.Insert(processingChainTemplate.Count, template));
        }

        [Test]
        public void ShouldNotAllowToAddSecondIImageDisplayTemplateToChain()
        {
            // Given
            var template = new ImageDisplayTemplateTemplate();
            var processingChainTemplate = new ProcessingChainTemplate() { new ImageProviderTemplate(), new ImageDisplayTemplateTemplate() };

            // When
            // Then
            Assert.Throws<InvalidOperationException>(() => processingChainTemplate.Insert(1, template));
        }
    }
}