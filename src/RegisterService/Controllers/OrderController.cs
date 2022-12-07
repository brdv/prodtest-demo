using Microsoft.AspNetCore.Mvc;
using ProdtestApi.Models;
using ProdtestApi.Contracts;
using ProdtestApi.Services;

namespace ProdtestApi.Controllers;

public class OrdersController : ApiController
{
    private Dictionary<string, MenuItem> _items { get; }
    private IKitchenService _kitchenService { get; }

    public OrdersController(IKitchenService kitchenService)
    {
        // Stryker disable once all : non critical funcitonality
        _items = new Dictionary<string, MenuItem>{
            {"burger", new MenuItem(Guid.NewGuid(), "Burger", 5, 3.50)},
            {"fries", new MenuItem(Guid.NewGuid(), "Fries", 2, 1.50)},
            {"soda", new MenuItem(Guid.NewGuid(), "Soda", 1, 1.50)}
        };
        _kitchenService = kitchenService;

    }

    [HttpGet("items")]
    public IActionResult GetMenuItems()
    {
        var items = _items.Select(i => MapMenuItemToResponse(i.Value)).ToList();
        return Ok(items);
    }

    [HttpPost("")]
    public IActionResult PostOrder(CreateOrderRequest orderCreate)
    {
        var orderItems = orderCreate.ItemIds.Select(i => _items[i]).ToList();

        var order = new Order(
            Guid.NewGuid(),
            orderItems
        );

        var result = _kitchenService.handleOrder(order);

        return Ok(result);
    }

    private MenuItemResponse MapMenuItemToResponse(MenuItem item)
    {
        // Stryker disable once all : non critital functionality.
        return new MenuItemResponse(
            item.Name,
            item.PreparationTime,
            item.Price
        );
    }
}