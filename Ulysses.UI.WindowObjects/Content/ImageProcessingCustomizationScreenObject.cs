using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Windows;

namespace Ulysses.UI.WindowObjects.Content
{
    public class ImageProcessingCustomizationScreenObject : IScreenObject
    {
        private readonly Window _window;

        public ImageProcessingCustomizationScreenObject()
        {
            _window = new Window(By.Name("Shell"));
        }

        public bool IsDisplayed()
        {
            return _window.Exists;
        }
    }
}