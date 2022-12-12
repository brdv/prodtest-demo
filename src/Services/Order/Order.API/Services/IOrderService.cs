using Domain.Common.Contracts;
using Domain.Common.Models;

namespace Order.API.Services;

public interface IOrderService
{
    OrderModel AddOrder(CreateOrderRequest order);

    List<MenuItemResponse> GetMenuItems();
}