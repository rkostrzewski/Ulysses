﻿using System.Linq;
using System.Windows;

namespace Ulysses.App.Controls.DragAndDropExtension.Handlers
{
    public class DefaultDragHandler : IDragSource
    {
        public virtual void StartDrag(IDragInfo dragInfo)
        {
            var itemCount = dragInfo.SourceItems.Cast<object>().Count();

            if (itemCount == 1)
            {
                dragInfo.Data = dragInfo.SourceItems.Cast<object>().First();
            }
            else if (itemCount > 1)
            {
                dragInfo.Data = TypeUtilities.CreateDynamicallyTypedList(dragInfo.SourceItems);
            }

            dragInfo.Effects = dragInfo.Data != null ? DragDropEffects.Copy | DragDropEffects.Move : DragDropEffects.None;
        }
    }
}