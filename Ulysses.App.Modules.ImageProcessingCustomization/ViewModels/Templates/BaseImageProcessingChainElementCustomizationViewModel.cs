using System;
using System.Linq;
using Prism.Regions;
using Ulysses.App.Core.ViewModels;
using Ulysses.App.Modules.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates
{
    public abstract class BaseImageProcessingChainElementCustomizationViewModel<T> : NotifyPropertyChanged, INavigationAware
    {
        protected const string IdKey = nameof(IImageProcessingChainElement.Id);
        protected readonly IImageProcessingChainDataStore DataStore;
        protected T ChainElement;

        protected BaseImageProcessingChainElementCustomizationViewModel(IImageProcessingChainDataStore dataStore)
        {
            if (!typeof (IImageProcessingChainElement).IsAssignableFrom(typeof (T)))
            {
                throw new InvalidOperationException();
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
            OnPropertiesChanged();
        }

        protected abstract void OnPropertiesChanged();

        protected bool AreParametersCorrect(NavigationParameters parameters)
        {
            return parameters.Any(parameter => parameter.Key == IdKey && parameter.Value is string);
        }

        protected bool DoesStoreContainElement(string id)
        {
            return DataStore.ImageProcessingChainElements.Any(e => e.Id == id);
        }

        protected T RetrieveElementFromStore(NavigationContext navigationContext)
        {
            var id = (string)navigationContext.Parameters[IdKey];

            return DataStore.ImageProcessingChainElements.OfType<T>().FirstOrDefault(i => ((IImageProcessingChainElement)i).Id == id);
        }
    }
}