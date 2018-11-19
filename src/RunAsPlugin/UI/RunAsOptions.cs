using System;
using System.Windows.Forms;
using KeePass.Forms;
using KeePassLib;
using RunAsPlugin.Models;
using RunAsPlugin.SafeManagement;
using RunAsPlugin.UI.EventArgs;

namespace RunAsPlugin.UI
{
    /// <summary>
    /// Run As Options control for the password entry form.
    /// </summary>
    public partial class RunAsOptions : UserControl
    {
        public event EventHandler<IconUpdatedEventArgs> EntryIconUpdated;

        /// <summary>
        /// The filter options available in the browse dialog.
        /// </summary>
        private const string BROWSE_APPLICATION_FILTER = "Application (*.exe)|*.exe|All files (*.*)|*.*";

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
        /// Event handler triggered when the browse applicaiton button is clicked.
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
        /// Event handler triggered when the browse working dir button is clicked.
        /// </summary>
        /// <param name="sender">The object which triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void workingDirBrowse_Click(object sender, System.EventArgs e)
        {
            string currentWorkingDir = this.workingDir.Text;

            using (FolderBrowserDialog openDialog = new FolderBrowserDialog())
            {
                openDialog.SelectedPath = currentWorkingDir;

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    currentWorkingDir = openDialog.SelectedPath;
                    this.workingDir.Text = currentWorkingDir;
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
            string application = this.application.Text;
            PwCustomIcon customIcon = this.passwordEntryManager.SetIconFromExecutable(application);

            if (this.EntryIconUpdated != null)
            {
                IconUpdatedEventArgs eventArgs = new IconUpdatedEventArgs(customIcon);
                this.EntryIconUpdated(this, eventArgs);
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
            this.arguments.Enabled = isEnabled;
            this.workingDir.Enabled = isEnabled;
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
            this.arguments.Text = this.settings.Arguments;
            this.workingDir.Text = this.settings.WorkingDir;
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
            string arguments = this.arguments.Text;
            string workingDir = this.workingDir.Text;
            bool isNetOnly = this.netOnly.Checked;

            this.settings.IsEnabled = isEnabled;
            this.settings.Application = application;
            this.settings.Arguments = arguments;
            this.settings.WorkingDir = workingDir;
            this.settings.IsNetOnly = isNetOnly;

            this.passwordEntryManager.SetRunAsSettings(this.settings);
        }
    }
}
