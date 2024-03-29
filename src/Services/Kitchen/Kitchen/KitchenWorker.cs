﻿using System.Text;
using System.Text.Json;
using Domain.Common.Models;
using Kitchen.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Kitchen;

internal class KitchenWorker
{
    private readonly string RMQHost;
    private readonly IKitchenService _service;

    public KitchenWorker(IKitchenService service, IConfiguration config)
    {
        RMQHost = config["RMQ_HOST"] ?? "localhost";
        _service = service;
    }

    public void Run()
    {
        var factory = new ConnectionFactory() { HostName = RMQHost };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare($"dl-exchange-latest", ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName, exchange: $"dl-exchange-latest", routingKey: "");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var decodedBody = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<OrderModel>(decodedBody);
                if (order != null)
                {
                    _service.HandleOrder(order);
                }
                else
                {
                    Console.WriteLine("Something went wrong. Order is empty");
                }
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Press [enter] to exit.");
            while (true)
            {
            }
        }
    }
}
