using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.NonUniformityCorrection
{
    public class TwoPointNonUniformityCorrectionCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<TwoPointNonUniformityCorrectionTemplate>,
                                                                         ITwoPointNonUniformityCorrectionCustomizationViewModel
    {
        public TwoPointNonUniformityCorrectionCustomizationViewModel(IProcessingChainBuilderDataStore dataStore, ISelectFileCommand selectFileCommand) : base(dataStore)
        {
            SelectFileCommand = selectFileCommand;
            SelectFileCommand.OnFileSelected = selectedFile => { NonUniformityModelFilePath = selectedFile; };
        }

        public ISelectFileCommand SelectFileCommand { get; }

        public string Id => ChainElement?.Id;

        public string NonUniformityModelFilePath
        {
            get
            {
                return ChainElement.NonUniformityModelFilePath;
            }
            set
            {
                if (ChainElement.NonUniformityModelFilePath == value)
                {
                    return;
                }

                ChainElement.NonUniformityModelFilePath = value;
                OnPropertyChanged();
            }
        }
    }
}