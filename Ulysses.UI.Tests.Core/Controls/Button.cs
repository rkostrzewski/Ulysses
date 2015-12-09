using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace Ulysses.UI.Tests.Core.Controls
{
    public class Button : WinButton
    {
        public Button(UITestControl parent, params SearchCriteriaModel[] searchCriteria) : base(parent)
        {
            this.SetSearchProperties(searchCriteria);
        }

        public void Click()
        {
            Mouse.Click(this, new Point { X = -1, Y = -1 });
        }
    }
}