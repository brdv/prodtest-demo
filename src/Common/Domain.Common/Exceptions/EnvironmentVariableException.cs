using System.Runtime.Serialization;

namespace Domain.Common.Exceptions;

[Serializable]
public class EnvironmentVariableException : Exception
{
    protected EnvironmentVariableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public EnvironmentVariableException(string message) : base(message) { }
}
