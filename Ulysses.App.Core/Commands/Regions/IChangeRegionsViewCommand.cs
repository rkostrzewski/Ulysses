using System;

namespace Ulysses.App.Core.Commands.Regions
{
    public interface IChangeRegionsViewCommand<in T> : ICommand<T> where T : IConvertible
    {
    }
}