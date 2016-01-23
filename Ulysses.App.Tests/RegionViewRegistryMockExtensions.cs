using System;
using Moq;
using Prism.Regions;

namespace Ulysses.App.Tests
{
    public static class RegionViewRegistryMockExtensions
    {
        public static void VerifyViewWasRegisteredWithRegion(this Mock<IRegionViewRegistry> regionViewRegistry, string regionName, Times timesCalled)
        {
            regionViewRegistry.Verify(r => r.RegisterViewWithRegion(regionName, It.IsAny<Type>()), timesCalled);
        }
    }
}