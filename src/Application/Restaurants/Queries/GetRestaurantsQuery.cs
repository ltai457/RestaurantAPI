using Application.Common.Interface;
using Application.Restaurants.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application.Restaurants.Queries;

public class GetRestaurantsQuery
{

}
public class GetRestaurantsQueryHandler
{
    private readonly IApplicationDbContext _context;

    public GetRestaurantsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantDto>> Handle(GetRestaurantsQuery query)
    {
        return await _context.Restaurants
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Category = r.Category,
                HasDelivery = r.HasDelivery
            })
            .ToListAsync();
    }
}