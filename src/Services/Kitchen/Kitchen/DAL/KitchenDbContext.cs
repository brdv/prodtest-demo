﻿using Domain.Common.Models;
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
        var dotnetEnv = Environment.GetEnvironmentVariable("PRODTEST_VERSION");
        var env = dotnetEnv != null ? dotnetEnv : "development";
        var tableName = env == "latest" ? "HandledOrders" : "HandledOrdersShadow";

        modelBuilder.Entity<HandledOrder>().HasKey(x => x.Id);
        modelBuilder.Entity<HandledOrder>().ToTable(tableName);
        base.OnModelCreating(modelBuilder);
    }
}
