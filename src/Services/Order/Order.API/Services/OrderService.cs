using Domain.Common.Models;
using Domain.Common.Contracts;

namespace Order.API.Services;

public class OrderService : IOrderService
{
    private Dictionary<string, MenuItem> _items = new Dictionary<string, MenuItem>{
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

        return newOrder;
    }

    public List<MenuItemResponse> GetMenuItems()
    {
        return _items.Select(i => i.Value.ToResponse()).ToList();
    }
}