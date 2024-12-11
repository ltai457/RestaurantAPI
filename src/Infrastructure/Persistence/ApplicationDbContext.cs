// Infrastructure/Persistence/ApplicationDbContext.cs

using Application.Common.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User> , IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This is crucial - it sets up all Identity table configurations
        base.OnModelCreating(modelBuilder);

        // Your existing configurations
        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address, address =>
            {
                address.Property(a => a.City).IsRequired(false);
                address.Property(a => a.Street).IsRequired(false);
                address.Property(a => a.PostalCode).IsRequired(false);
            });

        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasPrecision(18, 2);
    }
}