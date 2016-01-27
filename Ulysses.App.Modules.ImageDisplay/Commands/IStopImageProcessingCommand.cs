using System;
using JetBrains.Annotations;
using Ulysses.App.Core.Commands;

namespace Ulysses.App.Modules.ImageDisplay.Commands
{
    public interface IStopImageProcessingCommand : INoParameterCommand
    {
        [CanBeNull]
        Action OnProcessingStop { get; set; }
    }
}