// See https://aka.ms/new-console-template for more information
using Domain.Common.Exceptions;
using Domain.Common.Models;
using Kitchen.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var versionSetting = Environment.GetEnvironmentVariable("DL_TAG_VERSION");
if (versionSetting == null)
{
    throw new EnvironmentVariableException("The environment variable 'DL_TAG_VERSION' was not set.'");
}


var kitchen = new KitchenService();

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare("dl-exchange", ExchangeType.Fanout);

    var queueName = channel.QueueDeclare().QueueName;

    channel.QueueBind(queue: queueName, exchange: "dl-exchange", routingKey: "");

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var decodedBody = Encoding.UTF8.GetString(body);
        var order = JsonSerializer.Deserialize<OrderModel>(decodedBody);
        if (order != null) kitchen.HandleOrder(order, versionSetting);
        else Console.WriteLine("Something went wrong. Order is empty");
    };

    channel.BasicConsume(queue: queueName,
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}
