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
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
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

            container.VerifyInstanceRegistered<IProcessingChainBuilderDataStore>(Times.Once());
            container.VerifyTypeRegistered<IImageProcessingChainElementViewLocator>(Times.Once());
            container.VerifyTypeRegistered<IChangeProcessingChainElementCustomizationRegionViewCommand>(Times.Once());
            container.VerifyTypeRegistered<IProcessingChainDragHandler>(Times.Once());
            container.VerifyTypeRegistered<IProcessingChainDropHandler>(Times.Once());
            container.VerifyTypeRegistered<IProcessingChainCustomizationViewModel>(Times.Once());
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
            regionViewRegistry.VerifyViewWasRegisteredWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                                 Times.AtLeastOnce());
        }
    }
}