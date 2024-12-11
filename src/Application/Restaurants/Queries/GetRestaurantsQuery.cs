using Application.Common.Interface;
using Application.Restaurants.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Restaurants.Queries;

public class GetRestaurantsQuery : IRequest<List<RestaurantDto>>
{
    
}
public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, List<RestaurantDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRestaurantsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RestaurantDto>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await _context.Restaurants.ToListAsync(cancellationToken);
        return _mapper.Map<List<RestaurantDto>>(restaurants);
    }
}
    
