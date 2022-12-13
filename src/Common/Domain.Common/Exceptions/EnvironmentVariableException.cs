namespace Domain.Common.Exceptions;

public class EnvironmentVariableException : Exception
{
    public EnvironmentVariableException(string message) : base(message) { }
}