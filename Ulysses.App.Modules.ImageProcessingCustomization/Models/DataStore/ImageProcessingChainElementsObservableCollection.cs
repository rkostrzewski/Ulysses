using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public class ImageProcessingChainElementsObservableCollection : ObservableCollection<IImageProcessingChainElement>
    {
        protected override void InsertItem(int index, IImageProcessingChainElement item)
        {
            item.Id = Guid.NewGuid().ToString();

            while (this.Any(i => i.Id == item.Id))
            {
                item.Id = Guid.NewGuid().ToString();
            }

            base.InsertItem(index, item);
        }
    }
}