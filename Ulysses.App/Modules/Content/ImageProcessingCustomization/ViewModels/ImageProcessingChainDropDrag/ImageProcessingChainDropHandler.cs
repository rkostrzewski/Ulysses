using System;
using System.Linq;
using System.Reflection;
using Ulysses.App.Controls.DragAndDropExtension;
using Ulysses.App.Controls.DragAndDropExtension.Handlers;
using Ulysses.App.Modules.Content.ImageProcessingCustomization.Models.Templates;

namespace Ulysses.App.Modules.Content.ImageProcessingCustomization.ViewModels.ImageProcessingChainDropDrag
{
    public class ImageProcessingChainDropHandler : DefaultDropHandler, IImageProcessingChainDropHandler
    {
        public override void DragOver(DropInfo dropInfo)
        {
            if (!(dropInfo.Data is IImageProcessingAlgorithmTemplate))
            {
                return;
            }

            if (IsInsertedAtStart(dropInfo) || IsInsertedAtEnd(dropInfo))
            {
                return;
            }
            
            base.DragOver(dropInfo);
        }

        public override void Drop(DropInfo dropInfo)
        {
            var extractedData = ExtractData(dropInfo.Data);
            var data = extractedData as object[] ?? extractedData.Cast<object>().ToArray();

            var insertIndex = dropInfo.InsertIndex;
            var destinationList = GetList(dropInfo.TargetCollection);

            if (Equals(dropInfo.DragInfo.VisualSource, dropInfo.VisualTarget))
            {
                var sourceList = GetList(dropInfo.DragInfo.SourceCollection);

                foreach (var index in data.Select(o => sourceList.IndexOf(o)).Where(index => index != -1))
                {
                    sourceList.RemoveAt(index);

                    if (Equals(sourceList, destinationList) && index < insertIndex)
                    {
                        --insertIndex;
                    }
                }
            }

            foreach (var objectConstructor in data.Select(objectToCopy => objectToCopy.GetType().GetConstructor(Type.EmptyTypes)))
            {
                if (objectConstructor == null)
                {
                    throw new InvalidOperationException();
                }

                var newObject = objectConstructor.Invoke(null);
                destinationList.Insert(insertIndex++, newObject);
            }
        }

        private static bool IsInsertedAtEnd(DropInfo dropInfo)
        {
            return dropInfo.InsertIndex == 0;
        }

        private static bool IsInsertedAtStart(DropInfo dropInfo)
        {
            return dropInfo.InsertIndex == dropInfo.TargetCollection.Cast<object>().Count();
        }
    }
}