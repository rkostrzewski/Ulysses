using System;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews;
using Ulysses.Core.Templates;

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

            return !ImageProcessingChainElementsViewDefinitions.ViewDefinitions.ContainsKey(viewRequester.GetType())
                       ? _defaultViewType
                       : ImageProcessingChainElementsViewDefinitions.ViewDefinitions[viewRequesterType];
        }
    }
}