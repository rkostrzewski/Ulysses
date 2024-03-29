using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop;
using Ulysses.Core.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels
{
    public interface IProcessingChainCustomizationViewModel
    {
        IList<IProcessingChainElementTemplate> AvailableImageProcessingAlgorithmTemplates { get; set; }
        ObservableCollection<IProcessingChainElementTemplate> ProcessingChainElements { get; }
        IProcessingChainElementTemplate SelectedProcessingChainElementTemplate { get; set; }
        IProcessingChainDragHandler DragHandler { get; }
        IProcessingChainDropHandler DropHandler { get; }
        IUpdateProcessingEngineCommand UpdateProcessingEngineCommand { get; }
        IRemoveItemFromProcessingChainCommand RemoveItemFromProcessingChainCommand { get; }
    }
}