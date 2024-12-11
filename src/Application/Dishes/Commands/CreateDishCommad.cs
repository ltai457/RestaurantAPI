using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Commands;

public class CreateDishCommad : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }
}

public class CreateDishCommadHandler : IRequestHandler<CreateDishCommad, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateDishCommadHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDishCommad request, CancellationToken cancellationToken)
    {
       var restaurant = await _context.Restaurants
           .FirstOrDefaultAsync(r => r.Id == request.RestaurantId);
       // Check if the restaurant exist or not 
       if (restaurant == null)
           throw new Exception($"Restaurant with id {request.RestaurantId} not found");
       var dish = _mapper.Map<Dish>(request);
       _context.Dishes.Add(dish);
       await _context.SaveChangesAsync(cancellationToken);
       return dish.Id;
    }
}