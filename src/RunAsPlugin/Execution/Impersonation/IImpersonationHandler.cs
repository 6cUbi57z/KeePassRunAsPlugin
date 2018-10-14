using RunAsPlugin.Models;

namespace RunAsPlugin.Execution.Impersonation
{
    internal interface IImpersonationHandler
    {
        void ExecuteApplication(string application, ExecutionSettings settings);
    }
}
