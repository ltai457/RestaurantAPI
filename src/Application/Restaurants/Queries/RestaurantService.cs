/*

using Application.Common.Interface;
using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;
using AutoMapper;
using Domain.Entities;

using Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Restaurants.Queries;

public class RestaurantService : BaseService,IRestaurantService
{
   public RestaurantService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
   {
      
   }
   public async Task<List<RestaurantDto>> GetAll()
   {
      var restaurants = await _context.Restaurants.ToListAsync();
      return _mapper.Map<List<RestaurantDto>>(restaurants);
   }

   public async Task<RestaurantDto> GetById(int id)
   {
      var restaurant = await _context.Restaurants.FindAsync(id);
      return _mapper.Map<RestaurantDto>(restaurant);
   }

   public async Task<List<RestaurantDto>> SearchByName(string name)
   {
      var restaurants = await _context.Restaurants
         .Where(r => r.Name.Contains(name))
         .ToListAsync();
      return _mapper.Map<List<RestaurantDto>>(restaurants);
   }

   public async Task<int> Create(CreateRestaurantCommand command)
   {
      var restaurant = _mapper.Map<Restaurant>(command);
      _context.Restaurants.Add(restaurant);
      await _context.SaveChangesAsync(default);
      return restaurant.Id;
   }

   public async Task Update(int id, UpdateRestaurantCommand command)
   {
      var restaurant = await _context.Restaurants.FindAsync(id);
      if (restaurant == null) throw new Exception("Restaurant not found");
        
      _mapper.Map(command, restaurant);
      await _context.SaveChangesAsync(default);
   }

   public async Task Delete(int id)
   {
      var restaurant = await _context.Restaurants.FindAsync(id);
      if (restaurant == null) throw new Exception("Restaurant not found");

      _context.Restaurants.Remove(restaurant);
      await _context.SaveChangesAsync(default);
   } 
   
   
}*/
