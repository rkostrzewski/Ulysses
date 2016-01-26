using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ulysses.UI.Tests.Base;
using Ulysses.UI.Tests.Core.Utils;
using Ulysses.UI.WindowObjects;
using Assert = NUnit.Framework.Assert;

namespace Ulysses.UI.Tests
{
    //[CodedUITest]
    public class ShellWindowTests : BaseTest
    {
        [TestMethod]
        public void ShouldAllowToCloseWindow()
        {
            // Given
            var mainWindow = new ShellScreenObject();

            // When
            mainWindow.CloseWindow();
            new Wait().Until(() => !TestedApp.Exists);

            // Then
            Assert.IsFalse(TestedApp.Exists);
        }

        [TestMethod]
        public void ShouldAllowToMaximizeWindow()
        {
            // Given
            var mainWindow = new ShellScreenObject();

            // When
            mainWindow.MaximizeWindow();
            new Wait().Until(() => TestedApp.Maximized);

            // Then
            Assert.IsTrue(TestedApp.Maximized);
        }

        [TestMethod]
        public void ShouldAllowToMinimizeWindow()
        {
            // Given
            var mainWindow = new ShellScreenObject();

            // When
            mainWindow.MinimizeWindow();

            // Then
            Assert.IsTrue(TestedApp.WaitForControlCondition(u => TestedApp.Minimized, 1000));
        }
    }
}