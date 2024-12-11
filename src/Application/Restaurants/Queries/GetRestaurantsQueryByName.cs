using Application.Common.Interface;
using Application.Restaurants.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SearchRestaurantsQuery : IRequest<List<RestaurantDto>>
{
    public string SearchPhrase { get; set; }
}

public class SearchRestaurantsQueryHandler : IRequestHandler<SearchRestaurantsQuery, List<RestaurantDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchRestaurantsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RestaurantDto>> Handle(SearchRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await _context.Restaurants
            .Where(r => r.Name.Contains(request.SearchPhrase))
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<RestaurantDto>>(restaurants);
    }
}