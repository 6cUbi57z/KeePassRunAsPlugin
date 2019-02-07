using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.UI;
using RunAsPlugin.UI;

namespace RunAsPlugin
{
    /// <summary>
    /// Plugin entry class for RunAs KeePass plugin.
    /// </summary>
    public sealed class RunAsPluginExt : Plugin
    {
        private WindowMonitor windowMonitor;

        public override string UpdateUrl
        {
            get
            {
                return "https://raw.githubusercontent.com/dale2507/KeePassRunAsPlugin/master/Version.txt";
            }
        }

        /// <summary>
        /// Initialises the plugin.
        /// </summary>
        /// <param name="host">The plugin host.</param>
        /// <returns>True if the plugin started successfully.</returns>
        public override bool Initialize(IPluginHost host)
        {
            // Start the window monitor so that we can modify new password entry windows.
            this.windowMonitor = new WindowMonitor(host);

            // Create the run as context menu item.
            ContextMenuStrip contextMenu = host.MainWindow.EntryContextMenu;
            contextMenu.Items.Add(new ToolStripSeparator());

            RunAsMenuItem runAsMenuItem = new RunAsMenuItem(host.MainWindow);
            runAsMenuItem.AddToContextMenu(host.MainWindow.EntryContextMenu);
            UIUtil.AssignShortcut(runAsMenuItem, Keys.Control | Keys.E);

            // Return true for a successful startup.
            return true;
        }
    }
}