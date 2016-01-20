using System;

namespace Ulysses.App.Utils.Commands.Regions
{
    public interface IViewLocator<in T>
    {
        Type GetViewType(T viewRequester);
    }
}