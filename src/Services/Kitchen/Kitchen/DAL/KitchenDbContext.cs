using Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.DAL;

public class KitchenDbContext : DbContext
{
    public KitchenDbContext(DbContextOptions options) : base(options)
    {
    }

    protected KitchenDbContext() { }

    public DbSet<HandledOrder> HandledOrders => Set<HandledOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HandledOrder>().HasKey(x => x.Id);
        modelBuilder.Entity<HandledOrder>().ToTable("HandledOrders");
        base.OnModelCreating(modelBuilder);
    }
}
