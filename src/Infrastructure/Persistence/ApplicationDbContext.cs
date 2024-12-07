using Application.Common.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;  // Not Infrastructures
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Address as owned entity with required properties
        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address, address =>
            {
                address.Property(a => a.City).IsRequired(false);
                address.Property(a => a.Street).IsRequired(false);
                address.Property(a => a.PostalCode).IsRequired(false);
            });

        // Configure decimal precision for Dish.Price
        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasPrecision(18, 2);  // 18 digits total, 2 decimal places
    }
}