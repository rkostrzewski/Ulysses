// <copyright file="DragInfoTest.cs">Copyright ©  2015</copyright>
using System;
using System.Windows.Input;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ulysses.App.Controls.DragAndDropExtension;

namespace Ulysses.App.Controls.DragAndDropExtension.Tests
{
    /// <summary>This class contains parameterized unit tests for DragInfo</summary>
    [PexClass(typeof(DragInfo))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class DragInfoTest
    {
        /// <summary>Test stub for .ctor(Object, MouseButtonEventArgs)</summary>
        [PexMethod]
        public DragInfo ConstructorTest(object sender, MouseButtonEventArgs e)
        {
            DragInfo target = new DragInfo(sender, e);
            return target;
            // TODO: add assertions to method DragInfoTest.ConstructorTest(Object, MouseButtonEventArgs)
        }
    }
}
