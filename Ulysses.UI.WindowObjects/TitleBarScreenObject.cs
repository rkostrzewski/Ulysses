using System.Threading;
using Ulysses.UI.Tests.Core;
using Ulysses.UI.Tests.Core.Controls;
using Ulysses.UI.Tests.Core.Windows;

namespace Ulysses.UI.WindowObjects
{
    public class TitleBarScreenObject : IScreenObject
    {
        public TitleBarScreenObject(Window window)
        {
            Close = new Button(window, By.Name("Close"));
            Maximize = new Button(window, By.Name("Maximise"));
            Minimize = new Button(window, By.Name("Minimise"));
        }

        public Button Close { get; }

        public Button Maximize { get; }

        public Button Minimize { get; }

        public bool IsDisplayed()
        {
            return Close.Exists && Maximize.Exists && Minimize.Exists;
        }
    }
}