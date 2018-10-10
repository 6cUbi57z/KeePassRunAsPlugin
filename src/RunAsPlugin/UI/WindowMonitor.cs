using KeePass.Forms;
using KeePass.UI;

namespace RunAsPlugin.UI
{
    internal class WindowMonitor
    {
        internal WindowMonitor()
        {
            GlobalWindowManager.WindowAdded += this.WindowCreated;
        }

        private void WindowCreated(object sender, GwmWindowEventArgs e)
        {
            if (e.Form is PwEntryForm)
            {
                PwEntryForm pwEntryForm = (PwEntryForm)e.Form;
                pwEntryForm.Shown += this.PasswordEntryFormShown;
            }
        }

        private void PasswordEntryFormShown(object sender, System.EventArgs e)
        {
            new RunAsTab((PwEntryForm)sender);
        }
    }
}
