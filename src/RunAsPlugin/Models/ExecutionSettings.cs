namespace RunAsPlugin.Models
{
    using KeePassLib.Security;

    /// <summary>
    /// Settings to be used when impersonating.
    /// </summary>
    public class ExecutionSettings
    {
        /// <summary>
        /// Gets or sets the fully qualified domain name of the user.
        /// </summary>
        /// <example>
        /// * MyDomain\MyUsername
        /// * MyUsername@MyDomain
        /// </example>
        public string FullUsername { get; set; }

        /// <summary>
        /// Gets the username part of the credentials.
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
        /// Gets the domain part of the credentials.
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
        /// Gets or sets the password for the credentials.
        /// </summary>
        /// <remarks>
        /// This is kept as a <see cref="ProtectedString"/> so that the contents remain protected in memory.
        /// </remarks>
        public ProtectedString Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the impersonation should use the "Net Only" option.
        /// </summary>
        /// <remarks>
        /// This executes the application using the current user's credentials but will use the impersonated credentials for network requests.
        /// </remarks>
        public bool NetOnly { get; set; }

        /// <summary>
        /// Gets or sets the application to execute.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the arguments to pass to the application.
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Gets or sets the working directory to start the application from.
        /// </summary>
        public string WorkingDir { get; set; }
    }
}
