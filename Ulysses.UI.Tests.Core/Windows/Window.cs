using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Ulysses.UI.Tests.Core.Controls;

namespace Ulysses.UI.Tests.Core.Windows
{
    public sealed class Window : WinWindow
    {
        // ReSharper disable once IdentifierTypo
        private const string HwndWrapper = "HwndWrapper";

        public Window(params SearchCriteriaModel[] searchCriteria)
        {
            this.SetSearchProperties(new[]
            {
                By.ClassName(HwndWrapper, true)
            });

            this.SetSearchProperties(searchCriteria);

            if (searchCriteria.Any(s => s.Property == SearchProperty.Name))
            {
                WindowTitles.Add(searchCriteria.First(s => s.Property == SearchProperty.Name).Value);
            }

            SetFocus();
        }
    }
}