using Domain.Common.Models;

namespace Kitchen.DAL;

public interface IKitchenRepository
{
    Task AddHandledOrder(HandledOrder order);
}
