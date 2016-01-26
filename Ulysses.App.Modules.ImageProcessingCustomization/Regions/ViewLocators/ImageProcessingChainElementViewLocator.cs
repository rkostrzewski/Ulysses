using System;
using System.Collections.Generic;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Utilities;
using Ulysses.Core.Templates;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators
{
    public class ImageProcessingChainElementViewLocator : IImageProcessingChainElementViewLocator
    {
        private readonly Type _defaultViewType;

        public ImageProcessingChainElementViewLocator()
        {
            _defaultViewType = typeof (EmptyChainElementCustomizationView);
        }

        public Type GetViewType(IProcessingChainElementTemplate viewRequester)
        {
            var viewRequesterType = viewRequester.GetType();

            return !ImageProcessingChainElementsViewDefinitions.ViewDefinitions
                .ContainsKey(viewRequester.GetType()) ? _defaultViewType : ImageProcessingChainElementsViewDefinitions.ViewDefinitions[viewRequesterType];
        }
    }

    public static class ImageProcessingChainElementsViewDefinitions
    {
        public static IDictionary<Type, Type> ViewDefinitions { get; } = new Dictionary<Type, Type>
        {
            { typeof(ImageProviderTemplate), typeof(ImageProviderCustomizationView) },
            { typeof(TwoPointNonUniformityCorrectionTemplate), typeof(TwoPointNonUniformityCorrectionCustomizationView) },
            { typeof(SleeperTemplate), typeof(SleeperCustomizationView) }
        };
    }
}