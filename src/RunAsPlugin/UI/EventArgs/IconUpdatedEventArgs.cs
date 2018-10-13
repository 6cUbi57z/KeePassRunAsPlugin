using KeePassLib;

namespace RunAsPlugin.UI.EventArgs
{
    public class IconUpdatedEventArgs
    {
        internal IconUpdatedEventArgs(PwCustomIcon icon)
        {
            this.NewIcon = icon;
        }

        public PwCustomIcon NewIcon { get; }
    }
}
