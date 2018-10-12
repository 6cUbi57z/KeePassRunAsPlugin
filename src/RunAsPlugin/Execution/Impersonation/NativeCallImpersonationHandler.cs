using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using RunAsPlugin.Models;

namespace RunAsPlugin.Execution.Impersonation
{
    internal class NativeCallImpersonationHandler : IImpersonationHandler
    {
        public void ExecuteApplication(string application, ExecutionSettings settings)
        {
            // Determine the values to use on native calls based on the impersonation settings.
            string domain = settings.Domain;
            string username = settings.Username;
            Win32.LogonFlags logonFlags = settings.NetOnly ? Win32.LogonFlags.LOGON_NETCREDENTIALS_ONLY : Win32.LogonFlags.LOGON_WITH_PROFILE;

            // Create variables required for impersonation handling.
            //IntPtr token = IntPtr.Zero;
            Win32.StartupInfo startupInfo = new Win32.StartupInfo();
            Win32.ProcessInformation processInfo = new Win32.ProcessInformation();

            try
            {
                // Create process
                startupInfo.cb = Marshal.SizeOf(startupInfo);

                bool result = Win32.CreateProcessWithLogonW(
                    username,
                    domain,
                    settings.Password.ReadString(),
                    (uint)logonFlags,
                    application,
                    null,
                    0,
                    IntPtr.Zero,
                    null,
                    ref startupInfo,
                    out processInfo);

                if (!result)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (processInfo.process != IntPtr.Zero)
                {
                    Win32.CloseHandle(processInfo.process);
                }

                if (processInfo.thread != IntPtr.Zero)
                {
                    Win32.CloseHandle(processInfo.thread);
                }
            }
        }

        private class Win32
        {
            #region Enums
            [Flags]
            public enum CreationFlags
            {
                CREATE_SUSPENDED = 0x00000004,
                CREATE_NEW_CONSOLE = 0x00000010,
                CREATE_NEW_PROCESS_GROUP = 0x00000200,
                CREATE_UNICODE_ENVIRONMENT = 0x00000400,
                CREATE_SEPARATE_WOW_VDM = 0x00000800,
                CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            }

            [Flags]
            public enum LogonFlags
            {
                LOGON_WITH_PROFILE = 0x00000001,
                LOGON_NETCREDENTIALS_ONLY = 0x00000002
            }

            public enum LogonTypes
            {
                LOGON32_LOGON_INTERACTIVE = 2,
                LOGON32_LOGON_NETWORK = 3,
                LOGON32_LOGON_BATCH = 4,
                LOGON32_LOGON_SERVICE = 5,
                LOGON32_LOGON_UNLOCK = 7,
                LOGON32_LOGON_NEW_CREDENTIALS = 9,

                [Obsolete]
                LOGON32_LOGON_NETWORK_CLEARTEXT = 8
            }

            public enum LogonProviders
            {
                PROVIDER_DEFAULT = 0,
                PROVIDER_WINNT35 = 1,
                PROVIDER_WIN40 = 2, // NTLM
                PROVIDER_WIN50 = 3 // Negotiate
            }
            #endregion

            #region Win32 Methods
            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern int LogonUser(
                string lpszUserName,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern Boolean CreateProcessAsUser(
                IntPtr hToken,
                string lpApplicationName,
                string lpCommandLine,
                IntPtr lpProcessAttributes,
                IntPtr lpThreadAttributes,
                bool bInheritHandles,
                int dwCreationFlags,
                IntPtr lpEnvironment,
                string lpCurrentDirectory,
                ref StartupInfo lpStartupInfo,
                out ProcessInformation lpProcessInformation);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern Boolean CreateProcessWithLogonW(
                String userName,
                String domain,
                String password,
                UInt32 logonFlags,
                String applicationName,
                String commandLine,
                UInt32 creationFlags,
                IntPtr environment,
                String currentDirectory,
                ref StartupInfo startupInfo,
                out ProcessInformation processInformation);

            [DllImport("kernel32", SetLastError = true)]
            public static extern Boolean CloseHandle(IntPtr handle);
            #endregion

            #region structs
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct StartupInfo
            {
                public int cb;
                public String reserved;
                public String desktop;
                public String title;
                public int x;
                public int y;
                public int xSize;
                public int ySize;
                public int xCountChars;
                public int yCountChars;
                public int fillAttribute;
                public int flags;
                public UInt16 showWindow;
                public UInt16 reserved2;
                public byte reserved3;
                public IntPtr stdInput;
                public IntPtr stdOutput;
                public IntPtr stdError;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct ProcessInformation
            {
                public IntPtr process;
                public IntPtr thread;
                public int processId;
                public int threadId;
            }
            #endregion
        }
    }
}
