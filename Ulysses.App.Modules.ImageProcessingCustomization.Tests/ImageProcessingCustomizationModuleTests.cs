using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;
using Ulysses.App.Tests;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests
{
    [TestFixture]
    public class ImageProcessingCustomizationModuleTests
    {
        [Test]
        public void ShouldRegisterDependenciesWhenCreated()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();

            // When

            var module = new ImageProcessingCustomizationModule(regionViewRegistry.Object, container.Object);

            // Then

            container.VerifyInstanceRegistered<IImageProcessingChainDataStore>(Times.Once());
            container.VerifyTypeRegistered<IImageProcessingChainElementViewLocator>(Times.Once());
            container.VerifyTypeRegistered<IChangeImageProcessingChainElementCustomizationRegionViewCommand>(Times.Once());
            container.VerifyTypeRegistered<IImageProcessingChainDragHandler>(Times.Once());
            container.VerifyTypeRegistered<IImageProcessingChainDropHandler>(Times.Once());
            container.VerifyTypeRegistered<IImageProcessingCustomizationViewModel>(Times.Once());
            container.VerifyTypeRegistered<ITwoPointNonUniformityCorrectionCustomizationViewModel>(Times.Once());
        }

        [Test]
        public void ShouldRegisterViewsWhenInitialized()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();
            var module = new ImageProcessingCustomizationModule(regionViewRegistry.Object, container.Object);

            // When

            module.Initialize();

            // Then

            regionViewRegistry.VerifyViewWasRegisteredWithRegion(ApplicationRegion.ContentRegion.ToString(), Times.Once());
            regionViewRegistry.VerifyViewWasRegisteredWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), Times.Exactly(2));
        }
    }
}