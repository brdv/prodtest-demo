// Stryker disable all : Only RabbitMQ Specific implementation
using System.Text;
using RabbitMQ.Client;

namespace Domain.Common.Services;

public class RabbitMQService : IRabbitMQService
{
    public void PublishEvent(string hostname, string message, string queue)
    {
        var factory = new ConnectionFactory() { HostName = hostname };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(queue, ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: queue,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
