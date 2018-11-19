using KeePassLib.Security;

namespace RunAsPlugin.Models
{
    /// <summary>
    /// Settings to be used when impersonating.
    /// </summary>
    public class ExecutionSettings
    {
        /// <summary>
        /// The fully qualified domain name of the user.
        /// </summary>
        /// <example>
        /// * MyDomain\MyUsername
        /// * MyUsername@MyDomain
        /// </example>
        public string FullUsername { get; set; }

        /// <summary>
        /// The username part of the credentials.
        /// </summary>
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

        /// <summary>
        /// The domain part of the credentials.
        /// </summary>
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

        /// <summary>
        /// The password for the credentials.
        /// </summary>
        /// <remarks>
        /// This is kept as a <see cref="ProtectedString"/> so that the contents remain protected in memory.
        /// </remarks>
        public ProtectedString Password { get; set; }

        /// <summary>
        /// Determines if the impersonation should use the "Net Only" option.
        /// </summary>
        /// <remarks>
        /// This executes the application using the current user's credentials but will use the impersonated credentials for network requests.
        /// </remarks>
        public bool NetOnly { get; set; }

        /// <summary>
        /// The application to execute.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// The arguments to pass to the application.
        /// </summary>
        public string Arguments { get; set; }
    }
}
