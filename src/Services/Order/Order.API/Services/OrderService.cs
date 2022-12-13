using Domain.Common.Models;
using Domain.Common.Contracts;
using System.Text.Json;
using Domain.Common.Services;
using Domain.Common.Exceptions;

namespace Order.API.Services;

public class OrderService : IOrderService
{
    private readonly Dictionary<string, MenuItem> _items = new Dictionary<string, MenuItem>{
        {"burger", new MenuItem(Guid.NewGuid(), "Burger", 5, 3.50)},
        {"fries", new MenuItem(Guid.NewGuid(), "Fries", 2, 1.50)},
        {"soda", new MenuItem(Guid.NewGuid(), "Soda", 1, 1.50)}
    };
    public OrderModel AddOrder(CreateOrderRequest order)
    {
        // TODO: prettify
        List<MenuItem> menuItems = new List<MenuItem>();

        foreach (string item in order.MenuItems)
        {
            menuItems.Add(_items[item]);
        }

        OrderModel newOrder = new OrderModel(
            Guid.NewGuid(),
            menuItems
        );

        PublishNewOrder(newOrder);

        return newOrder;
    }

    private void PublishNewOrder(OrderModel order)
    {
        var serializedOrder = JsonSerializer.Serialize(order);

        var rmqHost = Environment.GetEnvironmentVariable("RMQ_HOST");
        var env = Environment.GetEnvironmentVariable("ENVIRONMENT");

        if (env == "DEVELOPMENT")
        {
            rmqHost = "localhost";
        }

        if (String.IsNullOrEmpty(rmqHost)) throw new EnvironmentVariableException("Doe het nou pls");

        RabbitMQService.PublishEvent(rmqHost, serializedOrder, "dl-exchange");
    }

    public List<MenuItemResponse> GetMenuItems()
    {
        return _items.Select(i => i.Value.ToResponse()).ToList();
    }
}