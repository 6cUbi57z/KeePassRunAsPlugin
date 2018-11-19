namespace RunAsPlugin.Execution.Impersonation
{
    using RunAsPlugin.SafeManagement;

    internal interface IImpersonationHandler
    {
        void ExecuteApplication(PasswordEntryManager entryManager);
    }
}
