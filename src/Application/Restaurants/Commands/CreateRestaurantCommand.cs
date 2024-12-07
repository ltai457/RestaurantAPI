using Application.Common.Interface;
using Domain.Entities;

namespace Application.Restaurants.Commands;

public class CreateRestaurantCommand
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public Address? Address { get; set; }
}

public class CreateRestaurantCommandHandler
{
    private readonly IApplicationDbContext _context;

    public CreateRestaurantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRestaurantCommand command)
    {
        var restaurant = new Restaurant
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            HasDelivery = command.HasDelivery,
            ContactEmail = command.ContactEmail,
            ContactNumber = command.ContactNumber,
            Address = command.Address
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync(default);
        return restaurant.Id;
    }
}