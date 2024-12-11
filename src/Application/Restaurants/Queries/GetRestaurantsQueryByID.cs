using Application.Common.Interface;
using Application.Restaurants.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Restaurants.Queries;

public class GetRestaurantsQueryByID: IRequest<RestaurantDto>
{
    public int RestaurantId { get; set; }
}

public class GetRestaurantByIdQuery : IRequest<RestaurantDto>
{
    public int Id { get; set; }
}

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRestaurantByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _context.Restaurants.FindAsync(request.Id);
        return _mapper.Map<RestaurantDto>(restaurant);
    }
}