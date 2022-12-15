using Domain.Common.Models;

namespace Kitchen.Services;

public record HandledOrder(
    Guid Id,
    Guid OrderId,
    int EstimatedPrepTime,
    int ActualPrepTime,
    string handler
);

public class KitchenService : IKitchenService
{
    private readonly int _speed = 9;

    public void HandleOrder(OrderModel order, string tag)
    {
        var prepXSpeed = order.TotalPrepTime * 10 / (double)_speed;
        var actualPrepTime = (int)(Math.Round(prepXSpeed, 0));

        var handled = new HandledOrder(Guid.NewGuid(), order.Id, order.TotalPrepTime, actualPrepTime, tag);

        Thread.Sleep(1000);

        Console.WriteLine(handled);
    }
}
