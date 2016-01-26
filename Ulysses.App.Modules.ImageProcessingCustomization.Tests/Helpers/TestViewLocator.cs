using System;
using Ulysses.App.Core.Commands.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Tests.Regions;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Helpers
{
    public class TestViewLocator : IViewLocator<IProcessingChainElementTemplate>
    {
        public Type GetViewType(IProcessingChainElementTemplate viewRequester)
        {
            return viewRequester.GetType() == typeof (TestChainElementTemplateTemplateTemplateTemplate) ? typeof (TestView) : typeof (DefaultView);
        }
    }
}