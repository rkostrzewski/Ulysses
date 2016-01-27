using System;
using Microsoft.Practices.Unity;
using Moq;

namespace Ulysses.App.Tests
{
    public static class UnityContainerMockExtensions
    {
        public static void VerifyTypeRegistered<T>(this Mock<IUnityContainer> container, Times timesCalled)
        {
            container.Verify(c => c.RegisterType(typeof (T), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>()),
                             timesCalled);
        }

        public static void VerifyInstanceRegistered<T>(this Mock<IUnityContainer> container, Times timesCalled)
        {
            container.Verify(c => c.RegisterInstance(typeof (T), It.IsAny<string>(), It.IsNotNull<object>(), It.IsAny<LifetimeManager>()), timesCalled);
        }
    }
}