using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<TwoPointNonUniformityCorrectionTemplate>, ITwoPointNonUniformityCorrectionCustomizationViewModel
    {
        public TwoPointNonUniformityCorrectionCustomizationViewModel(IImageProcessingChainDataStore dataStore) : base(dataStore)
        {
        }

        public string Id => ChainElement.Id;

        protected override void OnPropertiesChanged()
        {
            OnPropertyChanged(nameof(Id));
        }
    }
}
