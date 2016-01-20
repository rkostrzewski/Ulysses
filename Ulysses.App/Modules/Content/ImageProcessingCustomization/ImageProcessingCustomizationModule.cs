using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Regions.ViewLocators;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Regions;
using TwoPointNonUniformityCorrectionCustomizationView = Ulysses.App.Modules.Content.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection.TwoPointNonUniformityCorrectionCustomizationView;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization
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