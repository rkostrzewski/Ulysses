using System.Windows;
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ulysses.App.Controls.DragAndDropExtension.Adorners;

namespace Ulysses.App.Controls.DragAndDropExtension.Adorners.Tests
{
    /// <summary>This class contains parameterized unit tests for DropTargetAdorner</summary>
    [TestClass]
    [PexClass(typeof(DropTargetAdorner))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class DropTargetAdornerTest
    {

        /// <summary>Test stub for Create(Type, UIElement)</summary>
        [PexMethod]
        internal DropTargetAdorner CreateTest(Type type, UIElement adornedElement)
        {
            DropTargetAdorner result = DropTargetAdorner.Create(type, adornedElement);
            return result;
            // TODO: add assertions to method DropTargetAdornerTest.CreateTest(Type, UIElement)
        }

        /// <summary>Test stub for Detach()</summary>
        [PexMethod]
        public void DetachTest([PexAssumeNotNull]DropTargetAdorner target)
        {
            target.Detach();
            // TODO: add assertions to method DropTargetAdornerTest.DetachTest(DropTargetAdorner)
        }
    }
}
