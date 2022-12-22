using Domain.Common.Models;

namespace Kitchen.DAL;

public interface IKitchenRepository
{
    Task<HandledOrder> GetHandledOrder(Guid OrderId);
    Task AddHandledOrder(HandledOrder order);
}
