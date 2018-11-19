using RunAsPlugin.SafeManagement;

namespace RunAsPlugin.Execution.Impersonation
{
    internal interface IImpersonationHandler
    {
        void ExecuteApplication(PasswordEntryManager entryManager);
    }
}
