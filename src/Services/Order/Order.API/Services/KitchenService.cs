using Order.API.Models;

namespace Order.API.Services;

public class KitchenService : IKitchenService
{
    private static int _speed = 1;
    public OrderHandled handleOrder(OrderModel order)
    {
        // Stryker disable once all : non critital functionality.
        var actualPrepTime = order.TotalPrepTime / _speed;

        // Stryker disable once all : non critital functionality.
        Thread.Sleep(actualPrepTime * 100);

        var handled = new OrderHandled
        (
          order.Id,
          order.TotalPrepTime,
          actualPrepTime
        );

        return handled;
    }
}