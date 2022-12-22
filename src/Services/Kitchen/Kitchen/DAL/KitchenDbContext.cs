﻿using Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.DAL;

public class KitchenDbContext : DbContext
{
    public KitchenDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<HandledOrder> HandledOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HandledOrder>().HasKey(x => x.Id);
        builder.Entity<HandledOrder>().ToTable("HandledOrders");
        base.OnModelCreating(builder);
    }
}