using Domain.Entities;

namespace Infrastructure.Persistence;

// Create a Seeder class in Infrastructure/Persistence
public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;

    public DatabaseSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        if (!_context.Restaurants.Any())
        {
            

            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new() { Name = "Nashville Hot Chicken", Description = "Nashville Hot Chicken (10 pcs.)", Price = 10.30M },
                        new() { Name = "Chicken Nuggets", Description = "Chicken Nuggets (5 pcs.)", Price = 5.30M }
                    },
                    Address = new Address { City = "London", Street = "Cork St 5", PostalCode = "WC2N 5DU" }
                },
                new Restaurant
                {
                    
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address { City = "London", Street = "Boots 193", PostalCode = "W1F 8SR" }
                }
            };

            _context.Restaurants.AddRange(restaurants);
            await _context.SaveChangesAsync();
        }
    }
}

