using System;
using System.Collections.Generic;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.Regions.ViewLocators
{
    public class ImageProcessingChainElementViewLocator : IImageProcessingChainElementViewLocator
    {
        private readonly Type _defaultViewType;
        private readonly IDictionary<Type, Type> _viewDefinitions;

        public ImageProcessingChainElementViewLocator()
        {
            _defaultViewType = typeof (EmptyChainElementCustomizationView);
            _viewDefinitions = new Dictionary<Type, Type> { { typeof (TwoPointNonUniformityTemplate), typeof (TwoPointNonUniformityCorrectionCustomizationView) } };
        }

        public Type GetViewType(IImageProcessingChainElement viewRequester)
        {
            var viewRequesterType = viewRequester.GetType();

            return !_viewDefinitions.ContainsKey(viewRequester.GetType()) ? _defaultViewType : _viewDefinitions[viewRequesterType];
        }
    }
}