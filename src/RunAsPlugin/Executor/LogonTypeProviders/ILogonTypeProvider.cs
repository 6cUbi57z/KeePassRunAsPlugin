namespace RunAsPlugin.Executor.LogonTypeProviders
{
    internal interface ILogonTypeProvider
    {
        int LogonType { get; }

        int LogonProvider { get; }
    }
}
