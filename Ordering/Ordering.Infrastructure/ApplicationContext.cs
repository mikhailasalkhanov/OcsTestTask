using Microsoft.EntityFrameworkCore;
using Ordering.Domain;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure;

public class ApplicationContext : DbContext
{ 
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderLine>()
            .HasIndex(ol => new { ol.OrderId, ol.ProductId })
            .IsUnique();
    }
}