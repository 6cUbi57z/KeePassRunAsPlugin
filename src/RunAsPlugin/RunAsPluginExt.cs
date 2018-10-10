using KeePass.Plugins;

namespace RunAsPlugin
{
    public sealed class RunAsPluginExt : Plugin
    {
        private IPluginHost m_host = null;

        public override bool Initialize(IPluginHost host)
        {
            m_host = host;
            return true;
        }
    }
}