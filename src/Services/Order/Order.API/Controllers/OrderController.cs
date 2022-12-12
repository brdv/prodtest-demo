using Microsoft.AspNetCore.Mvc;
using Domain.Common.Contracts;
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
        var items = _orderService.GetMenuItems();

        return Ok(items);
    }

    [HttpPost("")]
    public IActionResult PostOrder(CreateOrderRequest orderCreate)
    {
        var order = _orderService.AddOrder(orderCreate);

        return Ok(order);
    }
}