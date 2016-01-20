using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Core.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using TwoPointNonUniformityCorrectionCustomizationView = Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection.TwoPointNonUniformityCorrectionCustomizationView;

namespace Ulysses.App.Modules.ImageProcessingCustomization
{
    public class ImageProcessingCustomizationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ImageProcessingCustomizationModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            RegisterModuleDependencies(container);
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ApplicationRegion.ContentRegion.ToString(), typeof (ImageProcessingCustomizationView));
            _regionManager.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(EmptyChainElementCustomizationView));
            _regionManager.RegisterViewWithRegion(ImageProcessingCustomizationViewRegions.ImageProcessingChainElementCustomizationRegion.ToString(), typeof(TwoPointNonUniformityCorrectionCustomizationView));
        }

        private static void RegisterModuleDependencies(IUnityContainer container)
        {
            container.RegisterInstance(typeof (IImageProcessingChainDataStore), new ImageProcessingChainDataStore());
            container.RegisterType<IImageProcessingChainElementViewLocator, ImageProcessingChainElementViewLocator>();
            container.RegisterType<IChangeImageProcessingChainElementCustomizationRegionViewCommand, ChangeImageProcessingChainElementCustomizationRegionViewCommand>();
            container.RegisterType<IImageProcessingChainDragHandler, ImageProcessingChainDragHandler>();
            container.RegisterType<IImageProcessingChainDropHandler, ImageProcessingChainDropHandler>();
            container.RegisterType<IImageProcessingCustomizationViewModel, ImageProcessingCustomizationViewModel>();
            container.RegisterType<ITwoPointNonUniformityCorrectionCustomizationViewModel, TwoPointNonUniformityCorrectionCustomizationViewModel>();
        }
    }
}