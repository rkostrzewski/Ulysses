using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Controls;
using Ulysses.UI.Tests.Core.Windows;

namespace Ulysses.UI.WindowObjects.Content
{
    public class ImageDisplayScreenObject : IScreenObject
    {
        private readonly Button _playButton;
        private readonly Window _window;

        public ImageDisplayScreenObject()
        {
            _window = new Window(By.Name("Shell"));
            _playButton = new Button(_window, By.Name("Play"));
        }

        public bool IsDisplayed()
        {
            return _window.Exists && _playButton.Exists;
        }
    }
}