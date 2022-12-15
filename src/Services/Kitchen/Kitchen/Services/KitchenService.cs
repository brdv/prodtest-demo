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
        double prepXSpeed = order.TotalPrepTime * 10 / _speed;
        var actualPrepTime = (int)(Math.Round(prepXSpeed, 0));
        // Console.WriteLine($"Order with id {order.Id} handled in {actualPrepTime}");
        var handled = new HandledOrder(Guid.NewGuid(), order.Id, order.TotalPrepTime, actualPrepTime, tag);

        Thread.Sleep(1000);

        Console.WriteLine(handled);
    }
}
