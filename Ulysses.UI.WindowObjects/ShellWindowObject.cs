using Ulysses.UI.WindowObjects.ControlPanel;

namespace Ulysses.UI.WindowObjects
{
    public class ShellScreenObject : BaseScreenObject
    {
        public ShellScreenObject()
        {
            ControlPanel = new ControlPanelScreenObject();
        }

        public ControlPanelScreenObject ControlPanel { get; private set; }
    }
}