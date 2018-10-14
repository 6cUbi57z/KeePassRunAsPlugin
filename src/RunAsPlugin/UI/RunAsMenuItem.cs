using System;
using System.Windows.Forms;
using KeePass.Forms;
using KeePassLib;
using RunAsPlugin.Execution;
using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.UI
{
    /// <summary>
    /// The context menu item which executes the application as the specified user.
    /// </summary>
    public class RunAsMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// The text displayed on the menu item.
        /// </summary>
        private const string MENU_ITEM_TEXT = "Run Application as User";

        /// <summary>
        /// The main KeePass window.
        /// </summary>
        private readonly MainForm mainWindow;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="mainWindow">The main KeePass window.</param>
        internal RunAsMenuItem(MainForm mainWindow)
            : base(MENU_ITEM_TEXT)
        {
            this.mainWindow = mainWindow;

            this.Click += this.RunAsMenuItem_Click;
        }

        /// <summary>
        /// Adds the menu item to the context menu and attaches the relevant event handlers.
        /// </summary>
        /// <param name="entryContextMenu">The context menu to add the menu item to.</param>
        internal void AddToContextMenu(ContextMenuStrip entryContextMenu)
        {
            entryContextMenu.Items.Add(this);
            entryContextMenu.Opening += this.EntryContextMenu_Opening;
        }

        /// <summary>
        /// Event handler triggered when the context menu containing this menu item is opened.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EntryContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PwEntry[] selectedEntries = this.mainWindow.GetSelectedEntries();

            if (selectedEntries == null || selectedEntries.Length == 0)
            {
                this.Enabled = false;
            }
            else if (selectedEntries.Length == 1)
            {
                PasswordEntryManager entryManager = new PasswordEntryManager(selectedEntries[0]);
                this.Enabled = entryManager.GetRunAsSettings().IsEnabled;
            }
            else
            {
                this.Enabled = true;
            }
        }

        /// <summary>
        /// Event handler triggered when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RunAsMenuItem_Click(object sender, System.EventArgs e)
        {
            // Obtain a list of all selected entries to execute and loop through them.
            PwEntry[] selectedEntries = this.mainWindow.GetSelectedEntries();

            // TODO: Add confirmation when executing more than one application.

            foreach (PwEntry entry in selectedEntries)
            {
                PasswordEntryManager entryManager = new PasswordEntryManager(entry);

                try
                {
                    // Create and run an executor for the password entry.
                    ApplicationExecutor executor = new ApplicationExecutor(entryManager);
                    executor.Run();
                }
                catch (ApplicationExecutionException ex)
                {
                    // If there was an error, display a suitable error message.
                    string errorMessage = string.Concat(
                        "Unable to execute application for password entry '", entryManager.GetTitle(), "':",
                        Environment.NewLine,
                        Environment.NewLine,
                        ex.Message);

                    MessageBox.Show(
                        this.mainWindow,
                        errorMessage,
                        "Run As Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
