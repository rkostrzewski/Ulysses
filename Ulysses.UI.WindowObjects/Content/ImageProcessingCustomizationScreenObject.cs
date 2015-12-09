using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Controls;
using Ulysses.UI.Tests.Core.Windows;

namespace Ulysses.UI.WindowObjects.Content
{
    public class ImageProcessingCustomizationScreenObject : IScreenObject
    {
        private readonly TextBlock _text;
        private readonly Window _window;

        public ImageProcessingCustomizationScreenObject()
        {
            _window = new Window(By.Name("Shell"));
            _text = new TextBlock(_window, By.Name("Hello", true));
        }

        public bool IsDisplayed()
        {
            return _window.Exists && _text.Exists;
        }
    }
}