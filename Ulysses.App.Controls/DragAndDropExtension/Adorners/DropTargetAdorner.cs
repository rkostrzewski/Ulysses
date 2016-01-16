using System;
using System.Windows;
using System.Windows.Documents;

namespace Ulysses.App.Controls.DragAndDropExtension.Adorners
{
    public abstract class DropTargetAdorner : Adorner
    {
        protected readonly AdornerLayer AdornerLayer;

        protected DropTargetAdorner(UIElement adornedElement) : base(adornedElement)
        {
            AdornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            AdornerLayer.Add(this);
            IsHitTestVisible = false;
        }

        public DropInfo DropInfo { get; set; }

        public void Detach()
        {
            AdornerLayer.Remove(this);
        }

        internal static DropTargetAdorner Create(Type type, UIElement adornedElement)
        {
            if (!typeof (DropTargetAdorner).IsAssignableFrom(type))
            {
                const string adornerDoesNotDeriveFromDropTargetAdornerMessage = "The requested adorner class does not derive from {0}.";
                throw new InvalidOperationException(string.Format(adornerDoesNotDeriveFromDropTargetAdornerMessage, nameof(DropTargetAdorner)));
            }

            var constructorInfo = type.GetConstructor(new[] { typeof (UIElement) });
            if (constructorInfo != null)
            {
                return (DropTargetAdorner)constructorInfo.Invoke(new object[] { adornedElement });
            }

            const string adornerDoesNotContainConstructorThatTakesUiElementOnly = "The requested adorner class does not contain constructor that takes {0} only.";
            throw new InvalidOperationException(string.Format(adornerDoesNotContainConstructorThatTakesUiElementOnly, nameof(UIElement)));
        }
    }
}