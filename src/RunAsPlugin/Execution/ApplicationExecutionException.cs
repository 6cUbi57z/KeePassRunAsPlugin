using System;

[Serializable]
public class ApplicationExecutionException : Exception
{
    public ApplicationExecutionException(string message) : base(message) { }

    public ApplicationExecutionException(string message, Exception inner) : base(message, inner) { }
}