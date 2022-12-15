namespace Domain.Common.Exceptions;

[Serializable]
public class EnvironmentVariableException : Exception
{
    public EnvironmentVariableException() { }

    public EnvironmentVariableException(string message) : base(message) { }

    public EnvironmentVariableException(string message, Exception innerException)
    : base(message, innerException) { }
}
