using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ulysses.UI.Tests.Base;
using Ulysses.UI.WindowObjects;
using Ulysses.UI.WindowObjects.ControlPanel;

namespace Ulysses.UI.Tests
{
    //[CodedUITest]
    public class ControlPanelTests : BaseTest
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