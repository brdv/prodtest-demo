namespace Domain.Common.Services;

public interface IRabbitMQService
{
    void PublishEvent(string hostname, string message, string queue);
}
