using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using KeePass.Forms;
using KeePass.UI;
using KeePassLib;
using RunAsPlugin.Models;
using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.UI
{
    /// <summary>
    /// Run As Options control for the password entry form.
    /// </summary>
    public partial class RunAsOptions : UserControl
    {
        /// <summary>
        /// The filter options available in the browse dialog.
        /// </summary>
        private const string BROWSE_APPLICATION_FILTER = "Application (*.exe)|*.exe|All files (*.*)|*.*";

        private readonly PwEntryForm entryForm;

        /// <summary>
        /// The manager used for interacting with the open password entry.
        /// </summary>
        private readonly PasswordEntryManager passwordEntryManager;

        /// <summary>
        /// The run as settings displayed in the control.
        /// </summary>
        private RunAsEntrySettings settings;

        /// <summary>
        /// Default contructor.
        /// </summary>
        /// <param name="entryForm">The password entry form.</param>
        /// <param name="passwordEntryManager">The manager used for interacting with the open password entry.</param>
        public RunAsOptions(PwEntryForm entryForm, PasswordEntryManager passwordEntryManager)
        {
            InitializeComponent();

            this.entryForm = entryForm;
            this.passwordEntryManager = passwordEntryManager;

            this.LoadSettings();
            entryForm.EntrySaving += this.Container_EntrySaving;
        }

        /// <summary>
        /// Event handler triggered when the password entry is being saved.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Container_EntrySaving(object sender, KeePass.Util.CancellableOperationEventArgs e)
        {
            this.SaveSettings();
        }

        #region UI Events
        /// <summary>
        /// Event handler triggered when the browse button is clicked.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void applicationBrowse_Click(object sender, System.EventArgs e)
        {
            string currentApplication = this.application.Text;

            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.InitialDirectory = currentApplication;
                openDialog.Filter = BROWSE_APPLICATION_FILTER;
                openDialog.RestoreDirectory = true;

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedApplication = openDialog.FileName;

                    this.application.Text = selectedApplication;
                }
            }
        }

        /// <summary>
        /// Event handler triggered when enable run as checkbox is toggled.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void enableRunAs_CheckedChanged(object sender, System.EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;
            this.SetRunAsEnabledState(isChecked);
        }

        /// <summary>
        /// Event handler triggered when the set icon button is clicked.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void setIcon_Click(object sender, System.EventArgs e)
        {
            const string CUSTOM_ICON_UUID_FORM_FIELD = "m_pwCustomIconID";

            string application = this.application.Text;
            PwCustomIcon customIcon = this.passwordEntryManager.SetIconFromExecutable(application);

            // TODO: Move all of this to the WindowMonitor so that the options aren't messing with their parent controls.
            try
            {
                // The password entry manager will set the UUID on the entry but when the user saves it will overwrite it.
                // We need to set the UUID value in the password form but it is a private class member.
                // HACK: Do some cheeky reflection to set the value of the private property.
                // WARNING: Likely to break!!!
                FieldInfo customIconUuidField = typeof(PwEntryForm).GetField(
                    CUSTOM_ICON_UUID_FORM_FIELD,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                customIconUuidField.SetValue(this.entryForm, customIcon.Uuid);

                // Now that we've updated the value, we need to update the image on the button.
                Button iconSelectonButton = this.GetIconButton();
                UIUtil.SetButtonImage(
                    iconSelectonButton,
                    ScaleImage(customIcon.GetImage(), 16, 16),
                    true);

                MessageBox.Show(this.ParentForm, "Password Entry icon updated.", "Icon Updated");
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this.ParentForm,
                    "The icon was updated for the password entry but there was an error while updating this form. Saving the entry will overwrite the new icon.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// Changes the state of the controls based on the enabled status of "Run As".
        /// </summary>
        /// <param name="isEnabled">The enabled status of "Run As".</param>
        private void SetRunAsEnabledState(bool isEnabled)
        {
            this.application.Enabled = isEnabled;
            this.applicationBrowse.Enabled = isEnabled;
            this.netOnly.Enabled = isEnabled;
            this.setIcon.Enabled = isEnabled;
        }

        /// <summary>
        /// Load the settings from the password entry manager in to the <see cref="this.settings"/> property and the control fields.
        /// </summary>
        private void LoadSettings()
        {
            this.settings = this.passwordEntryManager.GetRunAsSettings();

            this.enableRunAs.Checked = this.settings.IsEnabled;
            this.application.Text = this.settings.Application;
            this.netOnly.Checked = this.settings.IsNetOnly;

            this.SetRunAsEnabledState(this.settings.IsEnabled);
        }

        /// <summary>
        /// Updates the settings in the <see cref="this.settings"/> property and password entry from the control fields.
        /// </summary>
        private void SaveSettings()
        {
            bool isEnabled = this.enableRunAs.Checked;
            string application = this.application.Text;
            bool isNetOnly = this.netOnly.Checked;

            this.settings.IsEnabled = isEnabled;
            this.settings.Application = application;
            this.settings.IsNetOnly = isNetOnly;

            this.passwordEntryManager.SetRunAsSettings(this.settings);
        }

        private Button GetIconButton()
        {
            const string ENTRY_ICON_BUTTON_NAME = "m_btnIcon";

            Control[] matchedControls = this.entryForm.Controls.Find(ENTRY_ICON_BUTTON_NAME, true);
            if (matchedControls.Length == 0)
            {
                throw new System.Exception("Icon selector button not found.");
            }

            if (!(matchedControls[0] is Button))
            {
                throw new System.Exception("The control which was found is not a Button.");
            }

            return (Button)matchedControls[0];
        }
        private static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}
