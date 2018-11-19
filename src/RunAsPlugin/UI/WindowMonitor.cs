namespace RunAsPlugin.UI
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;
    using KeePass.Forms;
    using KeePass.Plugins;
    using KeePass.UI;
    using RunAsPlugin.SafeManagement;
    using RunAsPlugin.UI.EventArgs;
    using RunAsPlugin.Utils;

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

            RunAsTab runAsTab = new RunAsTab((PwEntryForm)sender, passwordEntryManager);
            runAsTab.EntryIconUpdated += this.RunAsTab_EntryIconUpdated;
        }

        private void RunAsTab_EntryIconUpdated(object sender, IconUpdatedEventArgs e)
        {
            const string CUSTOM_ICON_UUID_FORM_FIELD = "m_pwCustomIconID";
            const string ENTRY_ICON_BUTTON_NAME = "m_btnIcon";

            PwEntryForm entryForm = ((PwEntryForm)((Control)sender).FindForm());

            // Icon for the password entry has been updated by the run as tab so update the UI.
            try
            {
                // The password entry manager will set the UUID on the entry but when the user saves it will overwrite it.
                // We need to set the UUID value in the password form but it is a private class member.
                // HACK: Do some cheeky reflection to set the value of the private property.
                // WARNING: Likely to break!!!
                FieldInfo customIconUuidField = typeof(PwEntryForm).GetField(
                    CUSTOM_ICON_UUID_FORM_FIELD,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                customIconUuidField.SetValue(entryForm, e.NewIcon.Uuid);

                // Now that we've updated the value, we need to update the image on the button.
                Button iconSelectonButton = UIUtils.GetControlByName<Button>(entryForm, ENTRY_ICON_BUTTON_NAME);
                UIUtil.SetButtonImage(
                    iconSelectonButton,
                    ImageUtils.ScaleImage(e.NewIcon.GetImage(), 16, 16),
                    true);

                // Finally, update the password entry list.
                this.host.MainWindow.RefreshEntriesList();
                this.host.MainWindow.UpdateUI(false, null, false, null, true, null, true);

                MessageBox.Show(entryForm, "Password Entry icon updated.", "Icon Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    entryForm,
                    string.Concat("The icon was updated for the password entry but there was an error while updating this form. Saving the entry will overwrite the new icon.\n\nAdditional Details:\n", ex.Message),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
