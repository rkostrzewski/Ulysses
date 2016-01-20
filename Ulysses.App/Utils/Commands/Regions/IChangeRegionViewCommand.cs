using System;

namespace Ulysses.App.Utils.Commands.Regions
{
    public interface IChangeRegionsViewCommand<in T> : ICommand<T> where T : IConvertible
    {
    }
}