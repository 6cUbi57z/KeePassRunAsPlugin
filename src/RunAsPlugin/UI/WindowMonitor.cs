using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.UI
{
    /// <summary>
    /// Monitors for password entry windows created by KeePass to modify them with the run as settings.
    /// </summary>
    internal class WindowMonitor
    {
        /// <summary>
        /// The plugin host.
        /// </summary>
        /// <remarks>
        /// We have to store the host rather than the database in case the user opens an additional database.
        /// </remarks>
        private readonly IPluginHost host;

        internal WindowMonitor(IPluginHost host)
        {
            this.host = host;

            GlobalWindowManager.WindowAdded += this.WindowCreated;
        }

        /// <summary>
        /// Event handler for when a new window is created. Checks if this is a password entry window and, if so, attaches to the "shown" event.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WindowCreated(object sender, GwmWindowEventArgs e)
        {
            if (e.Form is PwEntryForm)
            {
                PwEntryForm pwEntryForm = (PwEntryForm)e.Form;
                pwEntryForm.Shown += this.PasswordEntryFormShown;
            }
        }

        /// <summary>
        /// Event handler for when a password entry window is shown. Adds the "Run As" tab.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PasswordEntryFormShown(object sender, System.EventArgs e)
        {
            PwEntryForm form = (PwEntryForm)sender;

            PasswordEntryManager passwordEntryManager = new PasswordEntryManager(this.host.Database, form);

            new RunAsTab((PwEntryForm)sender, passwordEntryManager);
        }
    }
}
