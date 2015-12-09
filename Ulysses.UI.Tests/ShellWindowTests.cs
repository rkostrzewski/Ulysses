using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ulysses.App.Tests.Base;
using Ulysses.UI.WindowObjects;
using Ulysses.UI.WindowObjects.ControlPanel;

namespace Ulysses.App.Tests
{
    [CodedUITest]
    public class ShellWindowTests : BaseTest
    {
        [TestMethod]
        public void ShouldAllowToNavigateToImageProcessingCustomization()
        {
            // Given
            var shell = new ShellScreenObject();

            // When

            var contentPage = shell.ControlPanel.NavigateTo(ContentRegions.ImageProcessingCustomization);

            // Then
            Assert.IsTrue(contentPage.IsDisplayed());
        }

        [TestMethod]
        public void ShouldAllowToNavigateToImageDisplay()
        {
            // Given
            var shell = new ShellScreenObject();

            // When

            var contentPage = shell.ControlPanel.NavigateTo(ContentRegions.ImageDisplay);

            // Then
            Assert.IsTrue(contentPage.IsDisplayed());
        }
    }
}