using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Windows;

namespace Ulysses.UI.WindowObjects
{
    public class BaseScreenObject : IScreenObject
    {
        private readonly Window _window;

        public BaseScreenObject()
        {
            _window = new Window(By.Name("Shell"));
            TitleBar = new TitleBarScreenObject(_window);
        }

        private TitleBarScreenObject TitleBar { get; }

        public bool IsDisplayed()
        {
            return _window.Exists;
        }

        public void CloseWindow()
        {
            TitleBar.Close.Click();
        }

        public void MaximizeWindow()
        {
            TitleBar.Maximize.Click();
        }

        public void MinimizeWindow()
        {
            TitleBar.Minimize.Click();
        }
    }
}