using Application.Restaurants;
using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;
using Application.Restaurants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetRestaurantsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetRestaurantByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<RestaurantDto>>> Search([FromQuery] string phrase)
    {
        var result = await _mediator.Send(new SearchRestaurantsQuery { SearchPhrase = phrase });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateRestaurantCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteRestaurantCommand { Id = id });
        return NoContent();
    }
}