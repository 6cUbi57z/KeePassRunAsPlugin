using System.Windows.Forms;
using KeePass.Forms;
using RunAsPlugin.Models;
using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.UI
{
    public partial class RunAsOptions : UserControl
    {
        private const string BROWSE_APPLICATION_FILTER = "Application (*.exe)|*.exe|All files (*.*)|*.*";

        private readonly PasswordEntryManager passwordEntryManager;

        private RunAsEntrySettings settings;

        public RunAsOptions(PwEntryForm container, PasswordEntryManager passwordEntryManager)
        {
            InitializeComponent();

            this.passwordEntryManager = passwordEntryManager;

            this.LoadSettings();

            container.EntrySaving += this.Container_EntrySaving;
        }

        private void Container_EntrySaving(object sender, KeePass.Util.CancellableOperationEventArgs e)
        {
            this.SaveSettings();
        }

        #region UI Events
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

        private void enableRunAs_CheckedChanged(object sender, System.EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;
            this.SetEnabledStateOfControls(isChecked);
        }

        private void setIcon_Click(object sender, System.EventArgs e)
        {

        }
        #endregion

        private void SetEnabledStateOfControls(bool enabledState)
        {
            this.application.Enabled = enabledState;
            this.applicationBrowse.Enabled = enabledState;
            this.netOnly.Enabled = enabledState;
            this.setIcon.Enabled = enabledState;
        }

        private void LoadSettings()
        {
            this.settings = this.passwordEntryManager.GetRunAsSettings();

            this.enableRunAs.Checked = this.settings.IsEnabled;
            this.application.Text = this.settings.Application;
            this.netOnly.Checked = this.settings.IsNetOnly;

            this.SetEnabledStateOfControls(this.settings.IsEnabled);
        }

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
    }
}
