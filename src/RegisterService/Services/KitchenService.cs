using ProdtestApi.Models;

namespace ProdtestApi.Services;

public class KitchenService : IKitchenService
{
    private static int _speed = 1;
    public OrderHandled handleOrder(Order order)
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