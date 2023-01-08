using Domain.Common.Models;

namespace Kitchen.DAL;

public class KitchenRepository : IKitchenRepository
{
    private readonly KitchenDbContext _context;

    public KitchenRepository(KitchenDbContext context)
    {
        _context = context;
    }

    public async Task AddHandledOrder(HandledOrder order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}
