using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace Ulysses.UI.Tests.Core.Controls
{
    public class TextBlock : WinControl
    {
        public TextBlock(UITestControl parent, params SearchCriteriaModel[] searchCriteria) : base(parent)
        {
            this.SetSearchProperties(searchCriteria);
        }
    }
}