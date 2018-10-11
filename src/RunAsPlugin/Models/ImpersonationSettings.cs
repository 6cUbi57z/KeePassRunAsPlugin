using KeePassLib.Security;

namespace RunAsPlugin.Models
{
    public class ImpersonationSettings
    {
        public string FullUsername { get; set; }

        public string Username
        {
            get
            {
                if (this.FullUsername.Contains(@"\"))
                {
                    return this.FullUsername.Substring(this.FullUsername.IndexOf(@"\") + 1);
                }
                else if (this.FullUsername.Contains("@"))
                {
                    return this.FullUsername.Substring(0, this.FullUsername.IndexOf("@"));
                }
                else
                {
                    return this.FullUsername;
                }
            }
        }

        public string Domain
        {
            get
            {
                if (this.FullUsername.Contains(@"\"))
                {
                    return this.FullUsername.Substring(0, this.FullUsername.IndexOf(@"\"));
                }
                else if (this.FullUsername.Contains("@"))
                {
                    return this.FullUsername.Substring(this.FullUsername.IndexOf("@") + 1);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ProtectedString Password { get; set; }

        public bool NetOnly { get; set; }
    }
}
