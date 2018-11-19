namespace RunAsPlugin.Models
{
    /// <summary>
    /// Settings on a password entry for this run as plugin.
    /// </summary>
    internal class RunAsEntrySettings
    {
        /// <summary>
        /// Determines if the "Run As" functionality is enabled.
        /// </summary>
        internal bool IsEnabled { get; set; }

        /// <summary>
        /// Determines the application to execute.
        /// </summary>
        internal string Application { get; set; }

        /// <summary>
        /// The arguments to pass to the application.
        /// </summary>
        internal string Arguments { get; set; }

        /// <summary>
        /// Determines if the impersonation should use the "Net Only" option.
        /// </summary>
        /// <remarks>
        /// This executes the application using the current user's credentials but will use the impersonated credentials for network requests.
        /// </remarks>
        internal bool IsNetOnly { get; set; }
    }
}
