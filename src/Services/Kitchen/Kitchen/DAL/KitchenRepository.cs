using Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.DAL;

public class KitchenRepository : IKitchenRepository
{
    private readonly KitchenDbContext _context;

    public KitchenRepository(KitchenDbContext context)
    {
        _context = context;
    }

    public async Task<HandledOrder> GetHandledOrder(Guid orderId)
    {
        var order = await _context.HandledOrders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception($"Order with id: {orderId} not found in database.");
        }

        return order;
    }

    public async Task AddHandledOrder(HandledOrder order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
        Console.WriteLine(order);
    }
}
