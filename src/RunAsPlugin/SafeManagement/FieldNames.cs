namespace RunAsPlugin.SafeManagement
{
    /// <summary>
    /// Helper for field names on a password entry.
    /// </summary>
    internal static class FieldNames
    {
        /// <summary>
        /// The field name for the title of the password entry.
        /// </summary>
        internal const string Title = "Title";

        /// <summary>
        /// The field name for the username of the password entry.
        /// </summary>
        internal const string Username = "UserName";

        /// <summary>
        /// The field name for the password of the password entry.
        /// </summary>
        internal const string Password = "Password";

        /// <summary>
        /// Helper for run as setting field names on a password entry.
        /// </summary>
        internal static class RunAs
        {
            /// <summary>
            /// The field name for the IsEnabled run as setting of the password entry.
            /// </summary>
            internal const string IsEnabled = "RunAs.IsEnabled";

            /// <summary>
            /// The field name for the Application run as setting of the password entry.
            /// </summary>
            internal const string Application = "RunAs.Application";

            /// <summary>
            /// The field name for the application arguments run as setting of the password entry.
            /// </summary>
            internal const string Arguments = "RunAs.Arguments";

            /// <summary>
            /// The field name for the NetOnly run as setting of the password entry.
            /// </summary>
            internal const string NetOnly = "RunAs.NetOnly";
        }
    }
}
