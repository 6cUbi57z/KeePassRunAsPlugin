using System;
using System.Windows.Forms;
using KeePass.Forms;
using KeePassLib;
using RunAsPlugin.Executor;

namespace RunAsPlugin.UI
{
    public class RunAsMenuItem : ToolStripMenuItem
    {
        private const string MENU_ITEM_TEXT = "Run Application as User";

        private readonly MainForm mainWindow;

        internal RunAsMenuItem(MainForm mainWindow)
            : base(MENU_ITEM_TEXT)
        {
            this.mainWindow = mainWindow;

            this.Click += this.RunAsMenuItem_Click;
        }

        private void RunAsMenuItem_Click(object sender, System.EventArgs e)
        {
            PwEntry[] selectedEntries = this.mainWindow.GetSelectedEntries();

            foreach (PwEntry entry in selectedEntries)
            {
                try
                {
                    ApplicationExecutor executor = new ApplicationExecutor(entry);
                    executor.Run();
                }
                catch (Exception ex)
                {
                    // TODO: Make exception more specific
                    // TODO: Log error
                    MessageBox.Show(
                        this.mainWindow,
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
