namespace RunAsPlugin.Executor.LogonTypeProviders
{
    internal static class LogonTypeProvider
    {
        internal static ILogonTypeProvider Interactive => new TypeProviders.Interactive();
        internal static ILogonTypeProvider NetOnly => new TypeProviders.NetOnly();


        private static class TypeProviders
        {

            internal class Interactive : ILogonTypeProvider
            {
                public int LogonType => 2; // LOGON32_LOGON_INTERACTIVE

                public int LogonProvider => 0; // LOGON32_PROVIDER_DEFAULT
            }

            internal class NetOnly : ILogonTypeProvider
            {
                public int LogonType => 9; // LOGON32_LOGON_NEW_CREDENTIALS

                public int LogonProvider => 3; // LOGON32_PROVIDER_WINNT50
            }
        }
    }
}
