using Application.Common.Interface;
using Application.Dishes.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Queries;

public class GetDishQuery : IRequest<List<DishDto>>  // Only implement one interface
{
    public int RestaurantId { get; set; }
}

public class GetDishQueryHandler : IRequestHandler<GetDishQuery, List<DishDto>>  // Match the return type
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDishQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DishDto>> Handle(GetDishQuery request, CancellationToken cancellationToken)
    {
        var dishes = await _context.Dishes
            .Where(d => d.RestaurantId == request.RestaurantId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<DishDto>>(dishes);
    }
}