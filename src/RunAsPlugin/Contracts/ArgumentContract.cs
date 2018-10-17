using System;

namespace RunAsPlugin.Contracts
{
    internal static class ArgumentContract
    {
        internal static void NotNull(string parameterName, object parameterValue)
        {
            if (parameterValue == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        internal static void StringNotNullOrEmpty(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(parameterName, "Value cannot be null or empty.");
            }
        }

        internal static void StringNotNullOrWhiteSpace(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(parameterName, "Value cannot be null, empty or whitespace.");
            }
        }
    }
}
