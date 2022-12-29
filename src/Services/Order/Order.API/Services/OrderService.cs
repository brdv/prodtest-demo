using System.Text.Json;
using Domain.Common.Contracts;
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
    private readonly IConfiguration _configuration;

    public OrderService(IRabbitMQService service, IConfiguration configuration)
    {
        _rmqService = service;
        _configuration = configuration;
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
        Console.WriteLine($"Tag: {_configuration["DL_INTERNAL_TAG"]}; RmqHost: {_configuration["RMQ_HOST"]} ");
        foreach (var d in _configuration.AsEnumerable())
        {
            Console.WriteLine($"{d.Key}: {d.Value}");
        }
        var serializedOrder = JsonSerializer.Serialize(order);
        var versionTag = _configuration["DL_INTERNAL_TAG"];

        _rmqService.PublishEvent(_configuration["RMQ_HOST"], serializedOrder, $"dl-exchange-{versionTag}");
    }
}
