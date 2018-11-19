using System;
using System.IO;
using RunAsPlugin.Execution.Impersonation;
using RunAsPlugin.Models;
using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.Execution
{
    internal class ApplicationExecutor
    {
        private readonly PasswordEntryManager entryManager;
        private readonly RunAsEntrySettings settings;

        internal ApplicationExecutor(PasswordEntryManager entryManager)
        {
            this.entryManager = entryManager;
            this.settings = this.entryManager.GetRunAsSettings();
        }

        public void Run()
        {
            if (!this.settings.IsEnabled)
            {
                throw new ApplicationExecutionException("Run As command not enabled.");
            }

            if (!File.Exists(this.settings.Application))
            {
                string errorMessage = string.Concat("Executable '", this.settings.Application, "' could be found.");
                throw new ApplicationExecutionException(errorMessage);
            }

            ExecutionSettings impersonationSettings = this.entryManager.GetExecutionSettings();

            if (string.IsNullOrWhiteSpace(impersonationSettings.Username))
            {
                throw new ApplicationExecutionException("No username present.");
            }

            if (impersonationSettings.Password.IsEmpty)
            {
                throw new ApplicationExecutionException("No password present.");
            }

            try
            {
                IImpersonationHandler impersonation = new NativeCallImpersonationHandler();
                impersonation.ExecuteApplication(this.entryManager);
            }
            catch (Exception ex)
            {
                throw new ApplicationExecutionException(ex.Message, ex);
            }
        }
    }
}
