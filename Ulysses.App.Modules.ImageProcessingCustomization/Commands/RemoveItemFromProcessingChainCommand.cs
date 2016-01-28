using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public class RemoveItemFromProcessingChainCommand : Command<IProcessingChainElementTemplate>, IRemoveItemFromProcessingChainCommand
    {
        private readonly IProcessingChainBuilderDataStore _dataStore;

        public RemoveItemFromProcessingChainCommand(IProcessingChainBuilderDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public override bool CanExecute(IProcessingChainElementTemplate parameter)
        {
            if (!_dataStore.ProcessingChainTemplate.Contains(parameter))
            {
                return false;
            }

            var index = _dataStore.ProcessingChainTemplate.IndexOf(parameter);

            return !(index == 0 || index == _dataStore.ProcessingChainTemplate.Count - 1);
        }

        public override void Execute(IProcessingChainElementTemplate parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new CannotExecuteCommandException(typeof(RemoveItemFromProcessingChainCommand));
            }

            _dataStore.ProcessingChainTemplate.Remove(parameter);
        }
    }
}