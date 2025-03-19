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

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            return restaurant == null ? null : new RestaurantDto { Id = restaurant.Id, Name = restaurant.Name };
        }

        public async Task CreateRestaurantAsync(RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant { Name = restaurantDto.Name };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(int id, RestaurantDto restaurantDto)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                restaurant.Name = restaurantDto.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
