using System;
using System.Collections;
using System.Linq;
using Ulysses.App.Controls.DragAndDropExtension;
using Ulysses.App.Controls.DragAndDropExtension.Handlers;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.DragAndDrop
{
    public class ProcessingChainDropHandler : DefaultDropHandler, IProcessingChainDropHandler
    {
        public override void DragOver(IDropInfo dropInfo)
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

        public override void Drop(IDropInfo dropInfo)
        {
            var extractedData = ExtractData(dropInfo.Data);
            var data = extractedData as object[] ?? extractedData.Cast<object>().ToArray();
            var destinationList = GetList(dropInfo.TargetCollection);
            var sourceList = GetList(dropInfo.DragInfo.SourceCollection);
            var isSourceCollectionSameAsTargetCollection = ReferenceEquals(sourceList, destinationList);

            var insertIndex = PrepareSourceList(dropInfo, data, sourceList, destinationList);

            InsertIntoDestinationList(data, destinationList, insertIndex, isSourceCollectionSameAsTargetCollection);
        }

        private static void InsertIntoDestinationList(object[] data, IList destinationList, int insertIndex, bool isSourceCollectionSameAsTargetCollection)
        {
            foreach (var element in data)
            {
                var objectToInsert = element;

                if (!isSourceCollectionSameAsTargetCollection)
                {
                    objectToInsert = CreateNewInstanceOfSameType(element.GetType());
                }

                destinationList.Insert(insertIndex++, objectToInsert);
            }
        }

        private static object CreateNewInstanceOfSameType(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                throw new ArgumentException(type.FullName);
            }

            return constructor.Invoke(null);
        }

        private static bool IsInsertedAtStart(IDropInfo dropInfo)
        {
            return dropInfo.InsertIndex == 0;
        }

        private static bool IsInsertedAtEnd(IDropInfo dropInfo)
        {
            return dropInfo.InsertIndex == dropInfo.TargetCollection.Cast<object>().Count();
        }
    }
}