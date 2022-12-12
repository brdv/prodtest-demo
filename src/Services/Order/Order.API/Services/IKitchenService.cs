using Order.API.Models;

namespace Order.API.Services;

public interface IKitchenService
{
    OrderHandled handleOrder(OrderModel order);
}