namespace RunAsPlugin.Execution
{
    using System;

    [Serializable]
    public class ApplicationExecutionException : Exception
    {
        public ApplicationExecutionException(string message)
            : base(message)
        {
            // Nothing to do.
        }

        public ApplicationExecutionException(string message, Exception inner)
            : base(message, inner)
        {
            // Nothing to do.
        }
    }
}