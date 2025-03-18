using FoodDeliveryBackend.Domain.DTO;

namespace FoodDeliveryBackend.Application.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
        Task CreateRestaurantAsync(RestaurantDto restaurantDto);
        Task UpdateRestaurantAsync(int id, RestaurantDto restaurantDto);
        Task DeleteRestaurantAsync(int id);
    }
}
