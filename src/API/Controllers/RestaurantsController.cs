// API/Controllers/RestaurantsController.cs

using Application.Common.Interface;
using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _service;

    public RestaurantsController(IRestaurantService service)
    {
        _service = service;
    }

    [HttpGet("GetAllRestaurants")]
    public async Task<ActionResult<List<RestaurantDto>>> GetAll()
        => Ok(await _service.GetAll());

    [HttpGet("GetRestaurantBy{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
        => Ok(await _service.GetById(id));

    [HttpGet("search")]
    public async Task<ActionResult<List<RestaurantDto>>> Search([FromQuery] string name)
        => Ok(await _service.SearchByName(name));

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateRestaurantCommand command)
        => Ok(await _service.Create(command));

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateRestaurantCommand command)
    {
        await _service.Update(id, command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}