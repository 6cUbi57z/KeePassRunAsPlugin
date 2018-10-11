using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using RunAsPlugin.Executor.LogonTypeProviders;
using RunAsPlugin.Models;

namespace RunAsPlugin.Executor
{
    internal class ImpersonationContext : IDisposable
    {
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private WindowsImpersonationContext impersonationContext;

        public ImpersonationContext(ImpersonationSettings impersonationSettings)
        {
            // Determine the values to use on native calls based on the impersonation settings.
            string domain = impersonationSettings.Domain;
            string username = impersonationSettings.Username;

            ILogonTypeProvider logonTypeProvider = impersonationSettings.NetOnly ? LogonTypeProvider.NetOnly : LogonTypeProvider.Interactive;
            int logonType = logonTypeProvider.LogonType;
            int logonProvider = logonTypeProvider.LogonProvider;

            // Create variables required for impersonation handling.
            WindowsIdentity tempWindowsIdentity = null;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                if (RevertToSelf())
                {
                    if (LogonUser(
                        username,
                        domain,
                        impersonationSettings.Password.ReadString(),
                        logonType,
                        logonProvider,
                        ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
        }

        private void UndoImpersonation()
        {
            this.impersonationContext?.Undo();
            this.impersonationContext = null;
        }

        public void Dispose()
        {
            this.UndoImpersonation();
        }

        #region Native DLLs
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(
            string lpszUserName,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(
            IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);
        #endregion
    }
}
