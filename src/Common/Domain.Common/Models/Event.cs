namespace Domain.Common.Models;

public class Event
{
    public string Message { get; }
    public string Publisher { get; }
    public string Version { get; }

    public Event(string message, string publisher, string version)
    {
        Message = message;
        Publisher = publisher;
        Version = version;
    }
}