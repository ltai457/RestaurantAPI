using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Commands;

public class UpdateDishCommand : IRequest<Unit>

{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }  
}

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDishCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await _context.Dishes
            .FirstOrDefaultAsync(d=>d.Id == request.Id && d.RestaurantId == request.RestaurantId);
        if (dish == null)
            throw new Exception("Dish not found");
        _mapper.Map(request, dish);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}