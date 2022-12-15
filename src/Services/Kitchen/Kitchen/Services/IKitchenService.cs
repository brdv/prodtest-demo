using Domain.Common.Models;

namespace Kitchen.Services;

public interface IKitchenService
{
    void HandleOrder(OrderModel order, string tag);
}
