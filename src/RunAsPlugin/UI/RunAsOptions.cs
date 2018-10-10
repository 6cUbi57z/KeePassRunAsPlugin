using System.Windows.Forms;
using KeePass.Forms;
using KeePassLib.Collections;
using KeePassLib.Security;

namespace RunAsPlugin.UI
{
    public partial class RunAsOptions : UserControl
    {
        private const string BROWSE_APPLICATION_FILTER = "Application (*.exe)|*.exe|All files (*.*)|*.*";

        private ProtectedStringDictionary entryStrings;

        public RunAsOptions(PwEntryForm container)
        {
            InitializeComponent();

            this.entryStrings = container.EntryStrings;

            this.LoadSettings();
        }

        private void enableRunAs_CheckedChanged(object sender, System.EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;

            ProtectedString protectedString = new ProtectedString(false, isChecked.ToString());
            this.entryStrings.Set(FieldNames.IsEnabled, protectedString);

            this.SetEnabledStateOfControls(isChecked);
        }

        private void application_TextChanged(object sender, System.EventArgs e)
        {
            string application = ((TextBox)sender).Text;

            ProtectedString protectedString = new ProtectedString(false, application);
            this.entryStrings.Set(FieldNames.Application, protectedString);
        }

        private void netOnly_CheckedChanged(object sender, System.EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;

            ProtectedString protectedString = new ProtectedString(false, isChecked.ToString());
            this.entryStrings.Set(FieldNames.NetOnly, protectedString);
        }

        private void SetEnabledStateOfControls(bool enabledState)
        {
            this.application.Enabled = enabledState;
            this.applicationBrowse.Enabled = enabledState;
            this.netOnly.Enabled = enabledState;
            this.setIcon.Enabled = enabledState;
        }

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

        private void LoadSettings()
        {
            string isEnabledString = this.entryStrings.Get(FieldNames.IsEnabled)?.ReadString();
            string application = this.entryStrings.Get(FieldNames.Application)?.ReadString();
            string isNetOnlyString = this.entryStrings.Get(FieldNames.NetOnly)?.ReadString();

            bool.TryParse(isEnabledString, out bool isEnabled);
            bool.TryParse(isNetOnlyString, out bool isNetOnly);

            this.enableRunAs.Checked = isEnabled;
            this.application.Text = application;
            this.netOnly.Checked = isNetOnly;

            this.SetEnabledStateOfControls(isEnabled);
        }
    }
}
