using System;
using JetBrains.Annotations;
using Ulysses.App.Core.Commands;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public interface ISelectFolderCommand : INoParameterCommand
    {
        [CanBeNull]
        Action<string> OnFolderSelected { get; set; }
    }
}