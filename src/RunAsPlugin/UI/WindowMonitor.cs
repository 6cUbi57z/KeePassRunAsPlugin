using KeePass.Forms;
using KeePass.UI;
using KeePassLib;

namespace RunAsPlugin.UI
{
    internal class WindowMonitor
    {
        private readonly PwDatabase database;

        internal WindowMonitor(PwDatabase database)
        {
            this.database = database;

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
            PwEntryForm form = (PwEntryForm)sender;

            PasswordEntryManager passwordEntryManager = new PasswordEntryManager(database, form);

            new RunAsTab((PwEntryForm)sender, passwordEntryManager);
        }
    }
}
