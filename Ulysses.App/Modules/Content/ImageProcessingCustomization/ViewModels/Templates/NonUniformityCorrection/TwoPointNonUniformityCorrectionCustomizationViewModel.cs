using Prism.Regions;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.DataStore;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel, INavigationAware, ITwoPointNonUniformityCorrectionCustomizationViewModel
    {
        private string _id;

        public TwoPointNonUniformityCorrectionCustomizationViewModel(IImageProcessingChainDataStore dataStore) : base(dataStore)
        {   
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                OnPropertyChanged();
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Id = (string)navigationContext.Parameters[IdKey];
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var parameters = navigationContext.Parameters;

            return AreParametersCorrect(parameters) && DoesStoreContainElement((string)parameters[IdKey]);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
