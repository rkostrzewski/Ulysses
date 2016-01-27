using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageDisplay.ViewModels;
using Ulysses.App.Tests;

namespace Ulysses.App.Modules.ImageDisplay.Tests
{
    [TestFixture]
    public class ImageDisplayModuleTests
    {
        [Test]
        public void ShouldRegisterDependenciesWhenCreated()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();

            // When

            var module = new ImageDisplayModule(regionViewRegistry.Object, container.Object);

            // Then

            container.VerifyTypeRegistered<IImageDisplayViewModel>(Times.Once());
        }

        [Test]
        public void ShouldRegisterViewsWhenInitialized()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();
            var module = new ImageDisplayModule(regionViewRegistry.Object, container.Object);

            // When

            module.Initialize();

            // Then

            regionViewRegistry.VerifyViewWasRegisteredWithRegion(ApplicationRegion.ContentRegion.ToString(), Times.Once());
        }
    }
}