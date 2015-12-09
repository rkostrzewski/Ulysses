using System;
using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Controls;
using Ulysses.UI.Tests.Core.Windows;
using Ulysses.UI.WindowObjects.Content;

namespace Ulysses.UI.WindowObjects.ControlPanel
{
    public class ControlPanelScreenObject : IScreenObject
    {
        private readonly Button _imageDisplay;
        private readonly Button _imageProcessingCustomization;
        private readonly Window _window;

        public ControlPanelScreenObject()
        {
            _window = new Window(By.Name("Shell"));
            _imageDisplay = new Button(_window, By.Name("Image Display"));
            _imageProcessingCustomization = new Button(_window, By.Name("Processing Customization"));
        }

        public bool IsDisplayed()
        {
            return _window.Exists && _imageDisplay.Exists && _imageProcessingCustomization.Exists;
        }

        public IScreenObject NavigateTo(ContentRegions contentRegions)
        {
            switch (contentRegions)
            {
                case ContentRegions.ImageDisplay:
                    _imageDisplay.Click();
                    return new ImageDisplayScreenObject();
                case ContentRegions.ImageProcessingCustomization:
                    _imageProcessingCustomization.Click();
                    return new ImageProcessingCustomizationScreenObject();
                default:
                    throw new ArgumentOutOfRangeException(nameof(contentRegions), contentRegions, null);
            }
        }
    }
}