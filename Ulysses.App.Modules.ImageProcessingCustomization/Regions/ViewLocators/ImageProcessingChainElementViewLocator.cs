using System;
using System.Collections.Generic;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators
{
    public class ImageProcessingChainElementViewLocator : IImageProcessingChainElementViewLocator
    {
        private readonly Type _defaultViewType;
        private readonly IDictionary<Type, Type> _viewDefinitions;

        public ImageProcessingChainElementViewLocator()
        {
            _defaultViewType = typeof (EmptyChainElementCustomizationView);
            _viewDefinitions = new Dictionary<Type, Type> { { typeof (TwoPointNonUniformityCorrectionTemplate), typeof (TwoPointNonUniformityCorrectionCustomizationView) } };
        }

        public Type GetViewType(IImageProcessingChainElement viewRequester)
        {
            var viewRequesterType = viewRequester.GetType();

            return !_viewDefinitions.ContainsKey(viewRequester.GetType()) ? _defaultViewType : _viewDefinitions[viewRequesterType];
        }
    }
}