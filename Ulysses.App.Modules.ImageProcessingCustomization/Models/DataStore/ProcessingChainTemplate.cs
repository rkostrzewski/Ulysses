using System;
using System.Collections.ObjectModel;
using System.Linq;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.Templates.ImageDisplay;
using Ulysses.Core.Templates;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore
{
    public class ProcessingChainTemplate : ObservableCollection<IProcessingChainElementTemplate>
    {
        private const int FirstElementIndex = 0;

        protected override void InsertItem(int index, IProcessingChainElementTemplate item)
        {
            if (index == FirstElementIndex)
            {
                ValidateInsertItemAtStart(item);
            }

            if (index == Count && index != FirstElementIndex)
            {
                ValidateInsertItemAtEnd(item);
            }

            ValidateDuplicateInsertOfSpecialElements(item);

            var itemId = item.Id;

            if (item.Id == null || this.Any(i => i.Id == item.Id))
            {
                do
                {
                    item.Id = Guid.NewGuid().ToString();
                } while (this.Any(i => i.Id == item.Id));
            }

            base.InsertItem(index, item);
        }

        private static void ValidateInsertItemAtStart(IProcessingChainElementTemplate item)
        {
            if (!(item is IImageProviderTemplate))
            {
                throw new InvalidOperationException();
            }
        }

        private static void ValidateInsertItemAtEnd(IProcessingChainElementTemplate item)
        {
            if (!(item is IImageDisplayTemplate))
            {
                throw new InvalidOperationException();
            }
        }

        private void ValidateDuplicateInsertOfSpecialElements(IProcessingChainElementTemplate item)
        {
            if (item is IImageProviderTemplate && this.Any(i => i is IImageProviderTemplate))
            {
                throw new InvalidOperationException();
            }

            if (item is IImageDisplayTemplate && this.Any(i => i is IImageDisplayTemplate))
            {
                throw new InvalidOperationException();
            }
        }

        public bool IsValid()
        {
            return this.Count >= 2;
        }
    }
}