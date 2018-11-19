namespace RunAsPlugin.UI
{
    using System;
    using System.Windows.Forms;
    using KeePass.Forms;
    using RunAsPlugin.SafeManagement;
    using RunAsPlugin.UI.EventArgs;

    /// <summary>
    /// Provides the "Run As" tab control in the password entry window.
    /// </summary>
    internal class RunAsTab : TabPage
    {
        public event EventHandler<IconUpdatedEventArgs> EntryIconUpdated;

        /// <summary>
        /// The text to display on the tab.
        /// </summary>
        private const string TAB_TITLE = "Run As";

        /// <summary>
        /// The name of the tab container to search for and add the tab page to.
        /// </summary>
        private const string TAB_CONTAINER_NAME = "m_tabMain";

        /// <summary>
        /// The manager used for interacting with the open password entry.
        /// </summary>
        private readonly PasswordEntryManager passwordEntryManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="container">The password entry form.</param>
        /// <param name="passwordEntryManager">The manager used for interacting with the open password entry.</param>
        internal RunAsTab(PwEntryForm container, PasswordEntryManager passwordEntryManager)
            : base(TAB_TITLE)
        {
            this.passwordEntryManager = passwordEntryManager;

            // Find the tab container and add this tab page to it.
            TabControl tabContainer = this.GetTabContainer(container);
            tabContainer.Controls.Add(this);

            // Add the run as options to this tab page.
            RunAsOptions optionsControl = new RunAsOptions(container, this.passwordEntryManager);
            optionsControl.EntryIconUpdated += this.OptionsControl_EntryIconUpdated;
            optionsControl.Dock = DockStyle.Fill;
            this.Controls.Add(optionsControl);
        }

        private void OptionsControl_EntryIconUpdated(object sender, IconUpdatedEventArgs e)
        {
            if (this.EntryIconUpdated != null)
            {
                this.EntryIconUpdated(sender, e);
            }
        }

        /// <summary>
        /// Find the tab container.
        /// </summary>
        /// <param name="container">The password entry form.</param>
        /// <returns>The tab container with the name defined in <see cref="TAB_CONTAINER_NAME"/>.</returns>
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
