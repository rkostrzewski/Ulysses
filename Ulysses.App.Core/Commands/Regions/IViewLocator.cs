using System;

namespace Ulysses.App.Core.Commands.Regions
{
    public interface IViewLocator<in T>
    {
        Type GetViewType(T viewRequester);
    }
}