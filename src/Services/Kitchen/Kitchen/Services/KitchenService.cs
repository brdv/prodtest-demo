using Domain.Common.Models;
using Kitchen.DAL;

namespace Kitchen.Services;

public class KitchenService : IKitchenService
{
    private readonly int _speed = 9;
    private IKitchenRepository _kitchenRepository;

    public KitchenService(IKitchenRepository kitchenRepository)
    {
        _kitchenRepository = kitchenRepository;
    }

    public void HandleOrder(OrderModel order, string tag)
    {
        var prepXSpeed = order.TotalPrepTime * 10 / (double)_speed;
        var actualPrepTime = (int)(Math.Round(prepXSpeed, 0));

        var handled = new HandledOrder(Guid.NewGuid(), order.Id, order.TotalPrepTime, actualPrepTime, tag);

        Thread.Sleep(1000);

        Console.WriteLine(handled);

        _kitchenRepository.AddHandledOrder(handled);

    }
}
