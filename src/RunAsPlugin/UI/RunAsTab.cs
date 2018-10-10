using System.Windows.Forms;
using KeePass.Forms;

namespace RunAsPlugin.UI
{
    internal class RunAsTab : TabPage
    {
        private const string TAB_TITLE = "Run As";
        private const string TAB_CONTAINER_NAME = "m_tabMain";

        private readonly PasswordEntryManager passwordEntryManager;

        internal RunAsTab(PwEntryForm container, PasswordEntryManager passwordEntryManager)
            : base(TAB_TITLE)
        {
            this.passwordEntryManager = passwordEntryManager;

            TabControl tabContainer = this.GetTabContainer(container);
            tabContainer.Controls.Add(this);

            RunAsOptions optionsControl = new RunAsOptions(container, this.passwordEntryManager);
            optionsControl.Dock = DockStyle.Fill;
            this.Controls.Add(optionsControl);
        }

        private TabControl GetTabContainer(PwEntryForm container)
        {
            Control[] matchedControls = container.Controls.Find(TAB_CONTAINER_NAME, true);
            if (matchedControls.Length == 0)
            {
                throw new System.Exception("Tab container not found.");
            }

            if (!(matchedControls[0] is TabControl))
            {
                throw new System.Exception("The control which was found is not a TabControl.");
            }

            return (TabControl)matchedControls[0];
        }
    }
}
