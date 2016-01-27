using System.Linq;
using Prism.Regions;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.Exceptions;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates
{
    public abstract class BaseImageProcessingChainElementCustomizationViewModel<T> : NotifyPropertyChanged, INavigationAware
    {
        protected const string IdKey = nameof(IProcessingChainElementTemplate.Id);
        protected readonly IProcessingChainBuilderDataStore DataStore;
        protected T ChainElement;

        protected BaseImageProcessingChainElementCustomizationViewModel(IProcessingChainBuilderDataStore dataStore)
        {
            if (!typeof (IProcessingChainElementTemplate).IsAssignableFrom(typeof (T)))
            {
                throw new InvalidProcessingChainElementException();
            }

            DataStore = dataStore;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var parameters = navigationContext.Parameters;

            return AreParametersCorrect(parameters) && DoesStoreContainElement((string)parameters[IdKey]);
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            ChainElement = RetrieveElementFromStore(navigationContext);
            OnPropertyChanged(string.Empty);
        }

        protected bool AreParametersCorrect(NavigationParameters parameters)
        {
            return parameters.Any(parameter => parameter.Key == IdKey && parameter.Value is string);
        }

        protected bool DoesStoreContainElement(string id)
        {
            return DataStore.ProcessingChainTemplate.Any(e => e.Id == id);
        }

        protected T RetrieveElementFromStore(NavigationContext navigationContext)
        {
            var id = (string)navigationContext.Parameters[IdKey];

            return DataStore.ProcessingChainTemplate.OfType<T>().FirstOrDefault(i => ((IProcessingChainElementTemplate)i).Id == id);
        }
    }
}