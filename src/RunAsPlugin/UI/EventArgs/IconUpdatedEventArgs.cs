namespace RunAsPlugin.UI.EventArgs
{
    using KeePassLib;

    public class IconUpdatedEventArgs
    {
        private readonly PwCustomIcon newIcon;

        internal IconUpdatedEventArgs(PwCustomIcon icon)
        {
            this.newIcon = icon;
        }

        public PwCustomIcon NewIcon
        {
            get
            {
                return this.newIcon;
            }
        }
    }
}
