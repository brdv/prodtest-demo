using Domain.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using Order.API.Services;

namespace Order.API.Controllers;

public class OrdersController : ApiController
{
    private IOrderService _orderService { get; }

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("items")]
    public IActionResult GetMenuItems()
    {
        var menuItems = _orderService.GetMenuItems();

        return Ok(menuItems);
    }

    [HttpPost("")]
    public IActionResult PostOrder(CreateOrderRequest orderCreate)
    {
        var createdOrder = _orderService.AddOrder(orderCreate);

        return Ok(createdOrder);
    }
}
