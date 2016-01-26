using System.Linq;
using NUnit.Framework;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Models.DataStore
{
    [TestFixture]
    public class ProcessingChainDataStoreTests
    {
        [Test]
        public void ShouldContainImageProviderAndImageDisplayTemplatesWhenCreated()
        {
            // Given
            // When
            var dataStore = new ProcessingChainDataStore();

            // Then
            Assert.AreEqual(2, dataStore.ProcessingChainTemplate.Count);
            Assert.IsTrue(dataStore.ProcessingChainTemplate.Any(i => i.GetType() == typeof (ImageProviderTemplate)));
            Assert.IsTrue(dataStore.ProcessingChainTemplate.Any(i => i.GetType() == typeof (ImageDisplayTemplateTemplate)));
        }
    }
}