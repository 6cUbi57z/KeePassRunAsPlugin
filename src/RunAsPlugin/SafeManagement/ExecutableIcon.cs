namespace RunAsPlugin.SafeManagement
{
    using System.Drawing;
    using System.Security.Cryptography;
    using KeePassLib;
    using RunAsPlugin.Utils;

    internal class ExecutableIcon
    {
        private string executable;

        internal ExecutableIcon(string executable)
        {
            this.executable = executable;
        }

        internal PwCustomIcon GetCustomIcon()
        {
            byte[] icon = this.ConvertIconToPng();
            PwUuid uuid = this.GetUuid(icon);

            PwCustomIcon customIcon = new PwCustomIcon(uuid, icon);
            return customIcon;
        }

        private byte[] ConvertIconToPng()
        {
            Icon icon = this.GetExecutableIcon();
            return ImageUtils.ConvertIconToPng(icon);
        }

        private Icon GetExecutableIcon()
        {
            return Icon.ExtractAssociatedIcon(this.executable);
        }

        private PwUuid GetUuid(byte[] data)
        {
            using (MD5 hasher = MD5.Create())
            {
                byte[] hash = hasher.ComputeHash(data);
                return new PwUuid(hash);
            }
        }
    }
}
