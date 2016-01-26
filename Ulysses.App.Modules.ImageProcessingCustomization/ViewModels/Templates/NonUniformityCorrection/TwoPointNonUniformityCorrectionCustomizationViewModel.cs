using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<TwoPointNonUniformityCorrectionTemplate>, ITwoPointNonUniformityCorrectionCustomizationViewModel
    {
        public TwoPointNonUniformityCorrectionCustomizationViewModel(IProcessingChainBuilderDataStore dataStore) : base(dataStore)
        {
        }

        public string Id => ChainElement?.Id;
    }
}
