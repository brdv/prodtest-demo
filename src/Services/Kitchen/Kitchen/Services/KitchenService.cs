using Domain.Common.Models;
using Kitchen.DAL;

namespace Kitchen.Services;

public class KitchenService : IKitchenService
{
    private readonly int _speed = 9;
    private readonly IKitchenRepository _kitchenRepository;

    public KitchenService(IKitchenRepository kitchenRepository)
    {
        _kitchenRepository = kitchenRepository;
    }

    public void HandleOrder(OrderModel order)
    {
        var prepXSpeed = order.TotalPrepTime * 10 / (double)_speed;
        var actualPrepTime = (int)(Math.Round(prepXSpeed, 0));

        var handled = new HandledOrder(Guid.NewGuid(), order.Id, order.TotalPrepTime, actualPrepTime);

        _kitchenRepository.AddHandledOrder(handled);
    }
}
