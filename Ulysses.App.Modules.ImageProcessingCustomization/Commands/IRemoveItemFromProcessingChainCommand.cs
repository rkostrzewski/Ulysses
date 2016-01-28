using Ulysses.App.Core.Commands;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public interface IRemoveItemFromProcessingChainCommand : ICommand<IProcessingChainElementTemplate>
    {
    }
}