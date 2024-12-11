using Application.Common.Interface;
using Application.Dishes.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Queries;

public class GetDishQueryByID:IRequest<DishDto>
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
}

public class GetDishQueryByIDHandler : IRequestHandler<GetDishQueryByID, DishDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDishQueryByIDHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DishDto> Handle(GetDishQueryByID request, CancellationToken cancellationToken)
    {
        var dish = await _context.Dishes
            .FirstOrDefaultAsync(d=>d.Id == request.Id && d.RestaurantId == request.RestaurantId);
        if (dish == null)
        {
            throw new Exception("Dish not found");
        }
        return _mapper.Map<DishDto>(dish);
    }
}