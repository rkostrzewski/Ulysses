using Microsoft.Practices.Unity;
using Prism.Regions;
using Ulysses.ProcessingEngine.Factories;

namespace Ulysses.App
{
    public class ContainerBootstrapper
    {
        public static void ConfigureDependencies(IUnityContainer container)
        {
            RegisterModels(container);
        }

        private static void RegisterModels(IUnityContainer container)
        {
            container.RegisterType<IRegionManager>();
            container.RegisterType<IProcessingEngineFactory, ProcessingEngineFactory>();
        }
    }
}