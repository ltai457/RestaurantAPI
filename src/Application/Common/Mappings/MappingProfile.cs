using System.Text.Encodings.Web;
using Application.Dishes.Dtos;
using AutoMapper;
using Domain.Entities;
using Application.Restaurants.Queries;

using Application.Restaurants.Commands;
using Application.Restaurants.Dtos;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));
        CreateMap<Dish, DishDto>();
        
        CreateMap<CreateRestaurantCommand, Restaurant>();
        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}