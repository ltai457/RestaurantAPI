using Application.Dishes.Commands;
using Application.Dishes.Dtos;
using Application.Dishes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DishDto>>> GetRestaurantDishes(int restaurantId)
    {
        var query = new GetDishQuery { RestaurantId = restaurantId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DishDto>> GetDishById(int restaurantId, int id)
    {
        var query = new GetDishQueryByID
        {
            Id = id,
            RestaurantId = restaurantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<DishDto>>> SearchDishes(
        int restaurantId,
        [FromQuery] string phrase)
    {
        var query = new GetDishQueryByName
        {
            word = phrase,
            RestaurantId = restaurantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateDish(int restaurantId, CreateDishCommad command)
    {
        command.RestaurantId = restaurantId; // Ensure dish is created for correct restaurant
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDishById), new { restaurantId, id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDish(int restaurantId, int id, UpdateDishCommand command)
    {
        command.Id = id;
        command.RestaurantId = restaurantId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDish(int restaurantId, int id)
    {
        await _mediator.Send(new DeleteDishCommand
        {
            Id = id,
            RestaurantId = restaurantId
        });
        return NoContent();
    }
}