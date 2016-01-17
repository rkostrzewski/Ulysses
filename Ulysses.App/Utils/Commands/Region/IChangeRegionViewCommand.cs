using System;

namespace Ulysses.App.Utils.Commands.Region
{
    public interface IChangeRegionViewCommand<in T> : ICommand<T> where T : IConvertible
    {
    }
}