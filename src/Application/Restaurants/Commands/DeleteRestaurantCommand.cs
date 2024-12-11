using Application.Common.Interface;
using MediatR;

namespace Application.Restaurants.Commands;

public class DeleteRestaurantCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteRestaurantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _context.Restaurants.FindAsync(request.Id);
        if (restaurant == null) throw new Exception("Restaurant not found");

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;  // Important: Must return Unit.Value
    }
}