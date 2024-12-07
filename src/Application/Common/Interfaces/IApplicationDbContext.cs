

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface;

public interface IApplicationDbContext
{
    DbSet<Restaurant> Restaurants { get; }
    DbSet<Dish> Dishes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken); 
}