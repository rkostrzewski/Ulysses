using System.Linq;
using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Utils.ViewModels;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.Templates
{
    public abstract class BaseImageProcessingChainElementCustomizationViewModel : NotifyPropertyChanged
    {
        protected readonly IImageProcessingChainDataStore DataStore;
        protected const string IdKey = nameof(IImageProcessingChainElement.Id);

        protected BaseImageProcessingChainElementCustomizationViewModel(IImageProcessingChainDataStore dataStore)
        {
            DataStore = dataStore;
        }

        protected bool AreParametersCorrect(NavigationParameters parameters)
        {
            return parameters.Any(parameter => parameter.Key == IdKey && parameter.Value is string);
        }

        protected bool DoesStoreContainElement(string id)
        {
            return DataStore.ImageProcessingChainElements.Any(e => e.Id == id);
        }
    }
}