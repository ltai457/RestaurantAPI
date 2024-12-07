using Application.Common.Interface;
using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;
using Application.Restaurants.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly CreateRestaurantCommandHandler _createHandler;
    private readonly GetRestaurantsQueryHandler _getHandler;

    public RestaurantsController(
        IApplicationDbContext context,
        CreateRestaurantCommandHandler createHandler,
        GetRestaurantsQueryHandler getHandler)
    {
        _context = context;
        _createHandler = createHandler;
        _getHandler = getHandler;
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetRestaurants()
    {
        var query = new GetRestaurantsQuery();
        var restaurants = await _getHandler.Handle(query);
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateRestaurant(CreateRestaurantCommand command)
    {
        var id = await _createHandler.Handle(command);
        return Ok(id);
    }
}