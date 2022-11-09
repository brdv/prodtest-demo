using ProdtestApi.Models;

namespace ProdtestApi.Services;

public class KitchenService : IKitchenService
{
    private static int _speed = 1;
    public OrderHandled handleOrder(Order order)
    {
        var handled = new OrderHandled
        (
          order.Id,
          order.TotalPrepTime,
          prepOrder(order)
        );

        return handled;
    }

    private int prepOrder(Order order)
    {
        var rand = new Random();
        var speed = (rand.NextDouble() + 1) / _speed;

        var time = speed * order.TotalPrepTime;

        return (int)Math.Round(time);
    }
}