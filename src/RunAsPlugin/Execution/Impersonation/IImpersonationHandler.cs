using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.Execution.Impersonation
{
    internal interface IImpersonationHandler
    {
        void ExecuteApplication(string application, PasswordEntryManager entryManager);
    }
}
