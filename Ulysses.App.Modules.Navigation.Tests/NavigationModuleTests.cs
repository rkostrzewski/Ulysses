using System;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.Navigation.Commands;
using Ulysses.App.Modules.Navigation.Models;
using Ulysses.App.Modules.Navigation.ViewModels;
using Ulysses.App.Tests.Core;

namespace Ulysses.App.Modules.Navigation.Tests
{
    [TestFixture]
    public class NavigationModuleTests
    {
        [Test]
        public void ShouldRegisterDependenciesWhenCreated()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();

            // When

            var module = new NavigationModule(regionViewRegistry.Object, container.Object);

            // Then

            container.VerifyInstanceRegistered<INavigationPanelState>(Times.Once());
            container.VerifyTypeRegistered<IChangeContentRegionsViewCommand>(Times.Once());
            container.VerifyTypeRegistered<IChangeCurrentRegionInNavigationPanelCommand>(Times.Once());
            container.VerifyTypeRegistered<INavigationPanelViewModel>(Times.Once());
        }

        [Test]
        public void ShouldRegisterViewsWhenInitialized()
        {
            // Given

            var regionViewRegistry = new Mock<IRegionViewRegistry>();
            var container = new Mock<IUnityContainer>();
            var module = new NavigationModule(regionViewRegistry.Object, container.Object);

            // When

            module.Initialize();

            // Then

            regionViewRegistry.VerifyViewWasRegisteredWithRegion(ApplicationRegion.NavigationPanelRegion.ToString(), Times.Once());
        }
    }
}