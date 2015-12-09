﻿using Microsoft.Practices.Unity;
using Ulysses.App.Modules.Content.ImageDisplay.ViewModels;
using Ulysses.App.Modules.ControlPanel.ViewModels;
using Ulysses.ProcessingEngine.ProcessingEngine.Factories;

namespace Ulysses.App
{
    public static class ContainerConfiguration
    {
        public static void ConfigureDependencies(this IUnityContainer container)
        {
            RegisterModels(container);
            RegisterViewModels(container);
        }

        private static void RegisterModels(IUnityContainer container)
        {
            container.RegisterType<IProcessingEngineFactory, ProcessingEngineFactory>();
        }

        private static void RegisterViewModels(IUnityContainer container)
        {
            container.RegisterType<IControlPanelViewModel, ControlPanelViewModel>();
            container.RegisterType<IImageDisplayViewModel, ImageDisplayViewModel>();
        }
    }
}