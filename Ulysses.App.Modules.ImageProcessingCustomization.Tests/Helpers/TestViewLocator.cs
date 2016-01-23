using System;
using Ulysses.App.Core.Commands.Regions;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Tests.Regions;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Tests.Helpers
{
    public class TestViewLocator : IViewLocator<IImageProcessingChainElement>
    {
        public Type GetViewType(IImageProcessingChainElement viewRequester)
        {
            return viewRequester.GetType() == typeof (TestChainElement) ? typeof (TestView) : typeof (DefaultView);
        }
    }
}