using AutoMapper;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>().ReverseMap();
        CreateMap<Menu, MenuDto>().ReverseMap();
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}