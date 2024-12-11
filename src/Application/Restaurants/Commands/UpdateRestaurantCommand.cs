using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Restaurants;

public class UpdateRestaurantCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Initialize with empty string
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; }
    public string? ContactEmail { get; set; }  // These can stay nullable
    public string? ContactNumber { get; set; }
    public Address? Address { get; set; }
}

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, Unit>  // Added Unit here too
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)  // Return Task<Unit>
    {
        var restaurant = await _context.Restaurants.FindAsync(request.Id);
        if (restaurant == null) throw new Exception("Restaurant not found");

        _mapper.Map(request, restaurant);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;  // Signal successful completion
    }
}