using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KeePassLib;
using RunAsPlugin.Models;

namespace RunAsPlugin.Executor
{
    internal class ApplicationExecutor
    {
        private readonly PasswordEntryManager entryManager;
        private readonly RunAsEntrySettings settings;

        internal ApplicationExecutor(PwEntry entry)
        {
            this.entryManager = new PasswordEntryManager(entry);
            this.settings = this.entryManager.GetRunAsSettings();
        }

        internal void Run()
        {
            if (!this.settings.IsEnabled)
            {
                string errorMessage = this.GetErrorMessage("Run As command not enabled");
                throw new Exception(errorMessage);
            }

            if (!File.Exists(this.settings.Application))
            {
                string errorMessage = this.GetErrorMessage("Executable '", this.settings.Application, "' could be found");
                throw new FileNotFoundException(errorMessage);
            }

            ImpersonationSettings impersonationSettings = this.entryManager.GetImpersonationSettings();

            if (string.IsNullOrWhiteSpace(impersonationSettings.Username))
            {
                string errorMessage = this.GetErrorMessage("No username present");
                throw new FileNotFoundException(errorMessage);
            }

            if (impersonationSettings.Password.IsEmpty)
            {
                string errorMessage = this.GetErrorMessage("No username present");
                throw new FileNotFoundException(errorMessage);
            }

            using (ImpersonationContext impersonator = new ImpersonationContext(impersonationSettings))
            {
                Process.Start(this.settings.Application);
            }
        }

        private string GetErrorMessage(params string[] args)
        {
            string[] argsToAdd = new string[] { " for password entry '", this.entryManager.GetTitle(), "'." };
            string[] allArgs = args.Concat(argsToAdd).ToArray();
            return string.Concat(allArgs);
        }
    }
}
