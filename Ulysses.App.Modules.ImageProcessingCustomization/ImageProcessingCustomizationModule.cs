using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Misc;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.PostProcessing;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.Utilities;
using Ulysses.App.Modules.ImageProcessingCustomization.Views;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Misc;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing;
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
            RegisterViewsWithRegions();
        }

        private static void RegisterModuleDependencies(IUnityContainer container)
        {
            RegisterModels(container);
            RegisterFactories(container);
            RegisterCommands(container);
            RegisterHandlers(container);
            RegisterViewModels(container);
        }

        private static void RegisterModels(IUnityContainer container)
        {
            container.RegisterInstance(typeof (IProcessingChainBuilderDataStore), new ProcessingChainDataStore());
            container.RegisterType<IImageProcessingChainElementViewLocator, ImageProcessingChainElementViewLocator>();
            container.RegisterType<IAvailableProcessingChainElements, AvailableProcessingChainElements>();
        }

        private static void RegisterFactories(IUnityContainer container)
        {
            container.RegisterType<INonUniformityModelProvider, NonUniformityModelProvider>();
            container.RegisterType<IImageProviderFactory, ImageProviderFactory>();
            container.RegisterType<IImageProcessingAlgorithmsFactory, ImageProcessingAlgorithmsFactory>();
            container.RegisterType<IImageProcessingChainFactory, ImageProcessingChainFactory>();
        }

        private static void RegisterCommands(IUnityContainer container)
        {
            container.RegisterType<IUpdateProcessingEngineCommand, UpdateProcessingEngineCommand>();
            container.RegisterType<ISelectFolderCommand, SelectFolderCommand>();
            container.RegisterType<ISelectFileCommand, SelectFileCommand>();
            container.RegisterType<IChangeProcessingChainElementCustomizationRegionViewCommand, ChangeProcessingChainElementCustomizationRegionViewCommand>();
            container.RegisterType<IRemoveItemFromProcessingChainCommand, RemoveItemFromProcessingChainCommand>();
        }

        private static void RegisterHandlers(IUnityContainer container)
        {
            container.RegisterType<IProcessingChainDragHandler, ProcessingChainDragHandler>();
            container.RegisterType<IProcessingChainDropHandler, ProcessingChainDropHandler>();
        }
        
        // TODO: Extension Methods
        private static void RegisterViewModels(IUnityContainer container)
        {
            container.RegisterType<IProcessingChainCustomizationViewModel, ProcessingChainCustomizationViewModel>();
            container.RegisterType<IImageProviderCustomizationViewModel, ImageProviderCustomizationViewModel>();
            RegisterNonUniformityCorrectionViewModels(container);
            RegisterPostProcessingViewModels(container);
        }

        private static void RegisterPostProcessingViewModels(IUnityContainer container)
        {
            container.RegisterType<ISleeperCustomizationViewModel, SleeperCustomizationViewModel>();
            container.RegisterType<ISaveImageCustomizationViewModel, SaveImageCustomizationViewModel>();
            container.RegisterType<IBrightnessAdjustmentCustomizationViewModel, BrightnessAdjustmentCustomizationViewModel>();
            container.RegisterType<IContrastAdjustmentCustomizationViewModel, ContrastAdjustmentCustomizationViewModel>();
            container.RegisterType<IGammaAdjustmentCustomizationViewModel, GammaAdjustmentCustomizationViewModel>();
            container.RegisterType<IDestripeCustomizationViewModel, DestripeCustomizationViewModel>();
            container.RegisterType<IBilateralFilterCustomizationViewModel, BilateralFilterCustomizationViewModel>();
            container.RegisterType<IHighDefinitionRangeDetailEnhancementCustomizationViewModel, HighDefinitionRangeDetailEnhancementCustomizationViewModel>();
        }

        private static void RegisterNonUniformityCorrectionViewModels(IUnityContainer container)
        {
            container.RegisterType<ITwoPointNonUniformityCorrectionCustomizationViewModel, TwoPointNonUniformityCorrectionCustomizationViewModel>();
            container.RegisterType<IConstantRangeNonUniformityCorrectionCustomizationViewModel, ConstantRangeNonUniformityCorrectionCustomizationViewModel>();
            container.RegisterType<IMidwayInfraredEqualizationCustomizationViewModel, MidwayInfraredEqualizationCustomizationViewModel>();
        }

        private void RegisterViewsWithRegions()
        {
            _regionViewRegistry.RegisterViewWithRegion(ApplicationRegion.ContentRegion.ToString(), typeof(ImageProcessingCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(EmptyChainElementCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(ImageProviderCustomizationView));
            RegisterNonUniformityCorrectionViews();
            RegisterPostProcessingCorrectionViews();
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(SleeperCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(SaveImageCustomizationView));
        }

        private void RegisterPostProcessingCorrectionViews()
        {
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (BrightnessAdjustmentCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (ContrastAdjustmentCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof(GammaAdjustmentCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (DestripeCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof(BilateralFilterCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                      typeof(HighDefinitionRangeDetailEnhancementCustomizationView));
        }

        private void RegisterNonUniformityCorrectionViews()
        {
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (TwoPointNonUniformityCorrectionCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof (ConstantRangeNonUniformityCorrectionCustomizationView));
            _regionViewRegistry.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(),
                                                       typeof(MidwayInfraredEqualizationCustomizationView));
        }
    }
}