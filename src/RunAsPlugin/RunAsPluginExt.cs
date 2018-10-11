using KeePass.Plugins;
using RunAsPlugin.UI;

namespace RunAsPlugin
{
    public sealed class RunAsPluginExt : Plugin
    {
        private IPluginHost m_host = null;

        private WindowMonitor windowMonitor;

        public override bool Initialize(IPluginHost host)
        {
            m_host = host;

            this.windowMonitor = new WindowMonitor(host.Database);

            RunAsMenuItem runAsMenuItem = new RunAsMenuItem(host.MainWindow);
            host.MainWindow.EntryContextMenu.Items.Add(runAsMenuItem);

            return true;
        }
    }
}