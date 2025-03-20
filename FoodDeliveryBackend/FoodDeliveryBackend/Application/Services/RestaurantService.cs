using FoodDeliveryBackend.Application.Services.Interfaces;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FoodDeliveryDbContext _context;
        public RestaurantService(FoodDeliveryDbContext context) { _context = context; }

    }
}
