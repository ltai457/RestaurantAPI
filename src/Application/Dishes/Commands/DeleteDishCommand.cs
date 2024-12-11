using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dishes.Commands;

public class DeleteDishCommand :IRequest<Unit>
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
}

public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteDishCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
      var dish = await _context.Dishes
          .FirstOrDefaultAsync(d => d.Id == request.Id && d.RestaurantId == request.RestaurantId);
      if (dish == null)
          throw new Exception("Dish not found");
      
      _context.Dishes.Remove(dish);
      await _context.SaveChangesAsync(cancellationToken);
      return Unit.Value;
        
    }
}