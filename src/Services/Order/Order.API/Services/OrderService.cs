using System.Text.Json;
using Domain.Common.Contracts;
using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.Common.Services;

namespace Order.API.Services;

public class OrderService : IOrderService
{
    private readonly Dictionary<string, MenuItem> _items = new Dictionary<string, MenuItem>{
        {"burger", new MenuItem(Guid.NewGuid(), "Burger", 5, 3.50)},
        {"fries", new MenuItem(Guid.NewGuid(), "Fries", 2, 1.50)},
        {"soda", new MenuItem(Guid.NewGuid(), "Soda", 1, 1.50)}
    };

    private readonly IRabbitMQService _rmqService;

    public OrderService(IRabbitMQService service)
    {
        _rmqService = service;
    }

    public OrderModel AddOrder(CreateOrderRequest order)
    {
        var menuItems = order.MenuItems.Select(item => _items[item]).ToList();

        var newOrder = new OrderModel(
            Guid.NewGuid(),
            menuItems
        );

        PublishNewOrder(newOrder);

        return newOrder;
    }

    public List<MenuItemResponse> GetMenuItems()
    {
        return _items.Select(i => i.Value.ToResponse()).ToList();
    }

    private void PublishNewOrder(OrderModel order)
    {
        var serializedOrder = JsonSerializer.Serialize(order);

        var rmqHost = Environment.GetEnvironmentVariable("RMQ_HOST");
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        var versionSetting = Environment.GetEnvironmentVariable("DL_TAG_VERSION");
        if (versionSetting == null)
        {
            throw new EnvironmentVariableException("The environment variable 'DL_TAG_VERSION' was not set.");
        }

        if (string.IsNullOrEmpty(env))
        {
            throw new EnvironmentVariableException("The environment variable 'DOTNET_ENVIRONMENT' was not set.");
        }

        if (env == "DEVELOPMENT")
        {
            rmqHost = "localhost";
        }

        if (string.IsNullOrEmpty(rmqHost))
        {
            throw new EnvironmentVariableException("The environment variable 'RMQ_HOST' was not set.");
        }

        var exchange = "dl-exchange";
        if (versionSetting != "Vcurrent")
        {
            exchange = $"exchange-{versionSetting}";
        }

        _rmqService.PublishEvent(rmqHost, serializedOrder, exchange);
    }
}
