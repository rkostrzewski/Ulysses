using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Utilities;
using Ulysses.App.Modules.ImageProcessingCustomization.Views;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Utilities;
using Ulysses.ImageProviders.Factories;
using Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection.NonUniformityModels;
using Ulysses.ProcessingAlgorithms.Factories;

namespace Ulysses.App.Modules.ImageProcessingCustomization
{
    public class ImageProcessingCustomizationModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public ImageProcessingCustomizationModule(IRegionViewRegistry regionViewRegistry, IUnityContainer container)
        {
            _regionViewRegistry = regionViewRegistry;
            RegisterModuleDependencies(container);
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegion.ContentRegion.ToString(), typeof (ImageProcessingCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (EmptyChainElementCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (TwoPointNonUniformityCorrectionCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (ImageProviderCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (SleeperCustomizationView));
        }

        private static void RegisterModuleDependencies(IUnityContainer container)
        {
            container.RegisterInstance(typeof (IProcessingChainBuilderDataStore), new ProcessingChainDataStore());
            container.RegisterType<IImageProcessingChainElementViewLocator, ImageProcessingChainElementViewLocator>();

            container.RegisterType<INonUniformityModelProvider, NonUniformityModelProvider>();
            container.RegisterType<IImageProviderFactory, ImageProviderFactory>();
            container.RegisterType<IImageProcessingAlgorithmsFactory, ImageProcessingAlgorithmsFactory>();
            container.RegisterType<IImageProcessingChainFactory, ImageProcessingChainFactory>();

            container.RegisterType<IUpdateProcessingEngineCommand, UpdateProcessingEngineCommand>();
            container.RegisterType<ISelectFolderCommand, SelectFolderCommand>();
            container.RegisterType<ISelectFileCommand, SelectFileCommand>();
            container.RegisterType<IChangeProcessingChainElementCustomizationRegionViewCommand, ChangeProcessingChainElementCustomizationRegionViewCommand>();

            container.RegisterType<IProcessingChainDragHandler, ProcessingChainDragHandler>();
            container.RegisterType<IProcessingChainDropHandler, ProcessingChainDropHandler>();

            container.RegisterType<IProcessingChainCustomizationViewModel, ProcessingChainCustomizationViewModel>();

            container.RegisterType<IImageProviderCustomizationViewModel, ImageProviderCustomizationViewModel>();
            container.RegisterType<ITwoPointNonUniformityCorrectionCustomizationViewModel, TwoPointNonUniformityCorrectionCustomizationViewModel>();
            container.RegisterType<ISleeperCustomizationViewModel, SleeperCustomizationViewModel>();
        }
    }
}