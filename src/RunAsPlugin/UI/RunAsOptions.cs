using System.Windows.Forms;
using KeePass.Forms;

namespace RunAsPlugin.UI
{
    public partial class RunAsOptions : UserControl
    {
        private const string BROWSE_APPLICATION_FILTER = "Application (*.exe)|*.exe|All files (*.*)|*.*";

        public RunAsOptions(PwEntryForm container)
        {
            InitializeComponent();
        }

        private void enableRunAs_CheckedChanged(object sender, System.EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;
            this.SetEnabledStateOfControls(isChecked);
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
    }
}
