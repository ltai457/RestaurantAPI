using Application.Common.Interface;
using Application.Dishes.Dtos;
using Application.Restaurants.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Queries;

public class GetDishQueryByName:IRequest<List<DishDto>>
{
    public string word { get; set; }
    public int RestaurantId { get; set; }
}

public class GetDishQueryByNameHandler : IRequestHandler<GetDishQueryByName, List<DishDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDishQueryByNameHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DishDto>> Handle(GetDishQueryByName request, CancellationToken cancellationToken)
    {
        var dishes = await _context.Dishes
            .Where(d => d.RestaurantId == request.RestaurantId && d.Name.Contains(request.word))
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<DishDto>>(dishes);
    }
}