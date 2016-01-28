using System.Collections.Generic;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models
{
    public interface IAvailableProcessingChainElements : IEnumerable<IProcessingChainElementTemplate>
    {
    }
}