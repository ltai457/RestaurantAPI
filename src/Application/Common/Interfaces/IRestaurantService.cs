using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;

namespace Application.Common.Interface;

public interface IRestaurantService
{
    ///Where you can add all interface that you want
    
    Task<List<RestaurantDto>> GetAll();
    Task<RestaurantDto> GetById(int id);
    Task<List<RestaurantDto>> SearchByName(string name);
    Task<int> Create(CreateRestaurantCommand command);
    Task Update(int id, UpdateRestaurantCommand command);
    Task Delete(int id);
}
    
    
    
