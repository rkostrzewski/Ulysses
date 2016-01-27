using System;
using JetBrains.Annotations;
using Ulysses.App.Core.Commands;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public interface ISelectFileCommand : INoParameterCommand
    {
        [CanBeNull]
        Action<string> OnFileSelected { get; set; }
    }
}